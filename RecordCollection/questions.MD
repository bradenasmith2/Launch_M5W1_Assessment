### Questions

#### How would you rate your comfort level and abilities when working with a SQL database? (3 points)
* Overall, I would say that my comfort level is around a 7.5/10, and here is why:
	* With coding in SQL I'm not very comfortable, it feels heavy and unintuitive in comparison to the tools I can use in Visual Studios like querying with EFCore LINQ.
	* With this being said, my comfort level working with 'SQL' in C# is much higher primarily because I have had more time to experiement with the tools that EFCore introduces to allow easier data manipulation.
	* If I were tasked with updating 5 albums, and each of them began with 'T', using methods like `.Update()` `.Where()` `.Find()`, will allow me to see the structure of the data I'm digging into, so I could type something like: `Albums.Update(album).Where(e => e.Title.StartsWith('T'))` Now I can easily see the flow of data that is being accessed, whereas in PostgreSQL working with only SQL I would have to use external tools such as documentation to help guide me to a command that would allow me to do this.

	* In Short, I'm relatively uncomfortable with SQL, but working with SQL objects in C# I am much more comfortable.


#### How do you familiarize yourself with a codebase you haven’t worked in before? (3 points)
* Heres the typical order of files that I review before anything:
	1. README. Is there one? Does it contain any releveant information? Did previous devs leave notes?
	2. Run it! If possible, I like to see what I'm working with from an outside perspective first, this may give me ideas or improvements.
		* During our 2-week project, I was one of the few to go check another groups app, and during this time I realized how benefical an outside perspective is, this led to many changes for both of our groups. Sometimes it becomes very difficult to see something small or even something that doesn't quite make sense, as we become 'nose-blind'. 
	3. Models. What am I working with here? What attributes do they have? Any methods?
	4. Views. What pages can users visit? What is the intent of the application, for instance can users CRUD?
	5. Controllers. This is typically the last place I check on the first run through, this lets me get a closer look at those views, and their hidden actions (for instance how CREATE functions with NEW but only NEW is a view).
* Now that I've looked around and have a good understanding of the application, I can begin work. For instance, during this assessment and other projects I will update my database after I've checked everything so that I know what I'm putting in the Db. This also lets me ensure that I have all of the properties I may need.