# Upcomming Movies

## Background

This is a very simple App for the purpose of delivering a list of upcomming movies to the user. The data is retrieved from [The Movie Database (TMDb)](https://www.themoviedb.org).

It was made to be compatible with Android and iOS devices and was buit on top of [Xamarin Platform](https://www.xamarin.com/) and [Xamarin.Forms Framework](https://www.xamarin.com/forms). It also uses [Prism Library](https://github.com/PrismLibrary/Prism) to take advantage of convenient API to support MVVM pattern and [Unity Container](https://github.com/unitycontainer/unity) as IoC/Dependecy Injection container.

The design of this app is based on **Domain-Driven-Design** aproach. The projects in the solution is briefly describled below:
 - **UpcommingMovies.Core.Domain** - Holds the Data Model and Services Contracts. It's technology agnostic and takes the minimum dependency as possible of any external libraries or frameworks.
 - **UpcommingMovies.Infra.TheMovieDb** - Implements the services describled by *Core.Domain* contracts. For this app context, this project basically serves as a client for [The Movie Database](https://www.themoviedb.org) web services.
 - **UpcommingMovies.Infra.TheMovieDb.Test** - Development time tests for *UpcommingMovies.Infra.TheMovieDb*.
 - **UpcommingMovies.Infra.IoC** - Takes care of services registration. Services contracts describled by *Core.Domain* project will be binding to his respective implementation here. This aproach takes advantage of the *Core.Domain*'s consumer (here, the UI projects) don't needs to meet the services implementation. The UI projects not even refer the *Infra.TheMovieDb* project.
 - **UpcommingMovies.UI** - This is the Xamarin.Forms portable project. It contains almost all UI logic and assets. 
 - **UpcommingMovies.UI.Droid** - Android platform project. This is the App project for Android Platform. It instantiate and execute *UpcommingMovies.UI*, no specific platform implementation was done for this App, that means the *UpcommingMovies.UI.Droid* didn't take any modification since was created from the template.
 - **UpcommingMovies.UI.iOS** - The iOS platform project. It's analogue to the *UpcommingMovies.UI.Droid* but for iOS platform. As the *UI.Droid* project, this one also didn't take any changes since it's creation from the template.

## How to build

I did this project using Visual Studio 15.3 (2017) on Windows 10 machine with all Android and iOS development stuff installed (all installed by Visual Studio installer). To build and debug iOS app, I connected my Windows box to a macOS Sierra with Xcode 9 and Visual Studio For Mac 7. In theory it will play nice on Visual Studio for Mac too, but I didn't test.

So assuming you already had a similar enviroment, open the solution, restore Nuget packages and try to build. If build fail (it's common to fail at first time) you may try going to solution explorer, expand *UpcommingMovies.UI* project and look for *.xaml* files, there will be three files of this kind there: One at root (App.xaml) and two others at Views folder (MainPage.xaml and MovieDetailPage.xaml). Try to modify this files, close the Visual Studio, re-open it and try to build again (by modifing *.xaml* files, Visual Studio will generate some missing code based on it).

To run and debug Android app, at solution explorer right-click *UpcommingMovies.UI.Droid*, click "*Set as startup project*" then hit F5 and *voilà*! For iOS app you must do the same but for *UpcommingMovies.UI.iOS* project. Don't forget for iOS, to build, run and debug from a Windows box you must been connected to a mac and both windows and mac need to meet some requirements. This requirements is describled at https://developer.xamarin.com/guides/cross-platform/getting_started/requirements/.

I ran and debugged the Android app on an Android 7 (API 25) VM (qemu, Android SDK default) and a Moto X device with Android 6 (API 23). For iOS app I used the simulator with iPhone 5s and iOS 10.3 on mac. Unfortunatelly I had no chance to test it on a iPhone device.
