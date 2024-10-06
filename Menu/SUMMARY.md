# Menu System
## This menu system i have developed completely on my own demonstrating and improving my functional programming skills. It spans from button action boilerplate code to UI animation code using LeanTween.
## I used Functional Programming a lot for this system (at least partial) to create many functions that take in functions along with chainable extension methods. In the end this turned out great.


Check out `UITransition.cs` to see a lot of the implementation of the TransitionInfo and the extensions with it. I will make many more helpful API methods in here but I think that file is one of my favorites for the time I have been working on it. I keep optimizing and now its gotten to a point where I am writing functions to change fields on the transition info, which is a cool thing to see and do, I started to realize that I learn a lot when I program in the functional paradigm.

Example:

```cs
var original = new TransitionInfo(/* initialization */);

var myModifications = original
    .Apply(i => i.Duration = 2f)
    .Apply(i => i.EaseType = EaseType.Linear)
    .Apply(i => i.EndAction = null)
    .SwapIntroAnimation();
```
