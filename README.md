[![Build status](https://ci.appveyor.com/api/projects/status/0oiw9f5woi46fjyo?svg=true)](https://ci.appveyor.com/project/tugrulelmas/abiokaapi) [![Build Status](https://travis-ci.org/tugrulelmas/AbiokaApi.svg?branch=master)](https://travis-ci.org/tugrulelmas/AbiokaApi)

# AbiokaApi

I've done this after 7 years passed with development. I wrote this project according to S.O.L.I.D principles.

##Covered things##
- Authentication 
- [Authorization](#authorization)
- [Validation](#validation)
- Inversion of Control
- CRUD Operations
- Repository Pattern
- RESTful Services
- Single Page Application
- [Aspect Oriented Programming](#aspect-oriented-programming)
- Object Oriented Programming

##Used Technologies##
- C#
- Asp.Net Web Api
- SQL Server
- FluentValidation
- Castle Windsor
- NHibernate
- Fluent NHibernate
- AngularJS
- Angular Material
- [Material Design Data Table](https://github.com/daniel-nagy/md-data-table)
- Gulp
- npm
- NUnit
- Moq

##Aspect Oriented Programming##
There are dynamic handlers and service interceptors for adding additional behavior to the RESTful or Application Service layer without modifying service codes.

#### 1. Dynamic Handler
It aims to add behavior for RESTful Service (AbiokaApi.Host application). These behaviors can be logging Http Request and Response messages, checking authentication etc. If you want to do something for every request or response, you should use a dynamic handler. 

##### Usage
Write a class that implements IDynamicHandler interface.
```csharp
    public class NhUnitOfWorkHandler : IDynamicHandler
    {
        private readonly IUnitOfWork unitOfWork;

        public short Order => 10;

        public NhUnitOfWorkHandler(IUnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
        }

        public void BeforeSend(IRequestContext requestContext) {
            if (!unitOfWork.IsInTransaction)
            {
                unitOfWork.BeginTransaction();
            }
        }

        public void AfterSend(IResponseContext responseContext) {
            if (unitOfWork.IsInTransaction)
            {
                unitOfWork.Commit();
            }
        }

        public void OnException(IExceptionContext exceptionContext) {
            if (unitOfWork.IsInTransaction)
            {
                unitOfWork.Rollback();
            }
        }
    }
```
Register this class with IoC container. To learn additional information about Lifestyles please read [this](https://github.com/castleproject/Windsor/blob/master/docs/lifestyles.md).
```csharp
 DependencyContainer.Container.Register<IDynamicHandler, NhUnitOfWorkHandler>(LifeStyle.PerWebRequest)
```
There are following dynamic handlers:

##### 1.1. AuthenticationHandler
It checks Http Request header to find Json Web Token and throws an exception, if there is no valid token and the action is not allowed for anonymous login.

##### 1.2. ExceptionHandler
It catches every exception and wraps it and then returns Http Response with specific Status Code and additional Header value.

##### 1.2. NhUnitOfWorkHandler
It opens a db transaction before calling service layer and commits this transaction after service layer response. If there is an exception, it rollbacks the transaction.

#### 2. Service Interceptors
It aims to add behavior for the application services (AbiokaApi.ApplicationService application).

##### 2.1 RoleValidationInterceptor
if the current user hasn't the role which is necessary for the application service method, it throws an exception. 

```csharp
internal class RoleValidationInterceptor : IServiceInterceptor
{
    private readonly ICurrentContext currentContext;

    public RoleValidationInterceptor(ICurrentContext currentContext) {
        this.currentContext = currentContext;
    }

    public int Order => 0;

    public void BeforeProceed(IInvocationContext context) {
        var attributes = context.Method.GetCustomAttributes(typeof(AllowedRole), true);
        if (attributes == null || attributes.Count() == 0)
            return;

        if(currentContext.Current.Principal.Roles == null)
            throw new DenialException("AccessDenied");


        var allowedRoles = (AllowedRole)attributes.First();
        if (currentContext.Current.Principal.Roles.Any(r => allowedRoles.Roles.Contains(r)))
            return;

        throw new DenialException("AccessDenied");
    }
}
```

## Validation
Creating a class that inherits CustomValidator<`parameter type`> is enough to validate the parameter type for every service method which has this parameter.

**Example**

```csharp
public interface IUserService : IReadService<User>
{
    User Add(AddUserRequest request);
}
```

```csharp
public class AddUserRequestValidator : CustomValidator<AddUserRequest>
{
    private readonly IUserSecurityRepository userSecurityRepository;

    public AddUserRequestValidator(IUserSecurityRepository userSecurityRepository) {
        this.userSecurityRepository = userSecurityRepository;

        RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
        RuleFor(r => r.Password).NotEmpty().WithMessage("IsRequired");
    }

    protected override void DataValidate(AddUserRequest instance, ActionType actionType) {
        var tmpUser = userSecurityRepository.GetByEmail(instance.Email);
        if (tmpUser != null)
            throw new DenialException("UserIsAlreadyRegistered", instance.Email);
    }
}
```

## Authorization

Only defining the roles with `AllowedRole` attributte for service method is sufficient for authorization. You may want to look at [RoleValidationInterceptor](#21-rolevalidationinterceptor)

**Example**
```csharp
public interface IUserService : IReadService<User>
{
     [AllowedRole("Admin", "SuperUser")]
     void Update(User entiy);
}
```
