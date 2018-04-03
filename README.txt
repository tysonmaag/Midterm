Practical Midterm Try 2
Tyson Maag
4/2/18

Okay, so this is the 2nd time I've tried to do this now. RGS has crashed on me too many times.

So the code is not finished. ALl the parts work great separately, but the integration of them was the hardest part for me.

The idea was to have them all run from the midaco portion, but I couldn't figure out a way to run the ANSYS code from the midaco portion.

I have included all the sample parts that I used to the midterm, but All the graded things that you need should be in midtermproject folder.

The NX portion is located there and the journal passes in the arguments, creates a new part, does the sketching etc. parameterized by a, L, and t, then creates the IGES.

The ANSYS portion was difficult, but I think is mostly working right. It successfully imports the IGES file, runs teh analysis, and writes out the max stress. I ran into problems
when I tried to run it multiple times with the change directory function. It says the DB log file is corrupt, or something like that... so no idea what to do about that.

THe Midaco portion works with the single max stress file, but I wasn't sure how to have it run everything. I should be configured to optimize correctly. 