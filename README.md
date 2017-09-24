# SearchUserFromGitHub
SearchUserFromGitHub is an application to test GitHUB connection and get user with most stargazer public repos.

> Used languages in application:
- C#
- JavaScript

> Used technologies and frameworks:
- Unity (Dependency Injection)
- Octokit (lib to connect with GIT)
- NUnit (Unit test)
- jQuery (lib for JavaScript)
- Angular.js (lib for JavaScript)
- .Net 4.7 MVC 5
- log4net (log events)

> Setup application:
This application uses boot user, and we do not need provide any other credentials, but we can in:
- [x] SearchUserFromGitHub project, web.config:
```ruby
    <add key="Login" value="" />
    <add key="Password" value="" />
```

- [x] SearchUserFromGitHub.Tests project, app.config:
```ruby
    <add key="Login" value="" />
    <add key="Password" value="" />
```

> How to use:
Please type some letters in text box and hit enter or press 'search' button, if user exists, will appear