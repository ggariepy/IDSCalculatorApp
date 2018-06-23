# IDSCalculatorApp
Sample Xamarin calculator app for Android
This lil' guy is my first Android application beyond the test application Visual Studio/Xamarin builds for you
the first time you create a new project.  There's nothing fancy; no scientific functions, no super-duper UI; 
just a plain four-function calculator that will produce results rounded out to 8 digits past the decimal point.

I had a lot of fun with this so I didn't mind spending several hours making it work more or less right and be feature-complete
with regard to all of the buttons I originally designed for it.  

Things I would change:
1. I don't like how the TextView control goes outside the right boundary of the TableLayout I used.  Couldn't figure
   out how to fix that in a reasonable amount of time.
2. Unlike a "real calculator" the display blanks after you hit an operation key.  I could probably fix this, but
   there's only so much time.  The reason why it does it is because the "register" (e.g. the display) is the holding
   spot for the currently calculated value, and retaining the display after hitting an operator button was making 
   for some strange results.  It's a little less than perfect, but it at least does the math right.
3. It sure ain't pretty.  I'm no artist.  Plus I had to learn a lot of stuff to make this work in a short amount of time,
   and pretty was well down the list of priorities.

You like this?  Let's talk.  I liked doing it.  I also like screwing around with electronics, Arduinos, Raspberry Pis, etc.  I'm no EE, 
but I can wire a circuit together from a schematic (most days) and I love working on automating all the things.

--Geoff
   
