# Upcomming Movies

This is a very simple App with purpose to deliver a list of upcomming movies to the user. The data is retrived from [The Movie Database (TMDb)](http://www.themoviedb.org).

It was made to be compatible with Android and iOS devices and was buit on top of [Xamarin Platform](http://www.xamarin.com/) and [Xamarin.Forms Framework](http://www.xamarin.com/forms). It also uses [Prism Library](http://github.com/PrismLibrary/Prism) to take advantage of convenient API to support MVVM pattern. 

The design of this app at all is based on **Domain-Driven-Design** aproach. The projects present in the solution is briefly describled below:
 - **UpcommingMovies.Core.Domain** -- This project holds the Data Model and Services Contracts. It made to be technology agnostic and it takes the minimum dependency as possible of any external libraries or frameworks.
 - **UpcommingMovies.Infra.TheMovieDb** -- Implements the services describled by *Core.Domain* contracts. In this app context, this project basically serves as a client for The Movie DB Web Service.
 - **UpcommingMovies.Infra.TheMovieDb.Test** -- Development time tests for *UpcommingMovies.Infra.TheMovieDb*.
 - **UpcommingMovies.Infra.IoC** -- Takes care of services registrations. Services contracts describled by *Core.Domain* project are attached to this respective implementation here. This aproach takes advantage of the *Core.Domain*'s consumer (here, the UI projects) don't needs to meet the services implamentation and the UI projects not even refer the *Infra.TheMovieDb* project.
