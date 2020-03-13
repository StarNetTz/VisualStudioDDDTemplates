# VisualStudioDDDTemplates
Starnet DDD templates for VS 2019
#### Usage
Copy StarnetDDD.zip from **templates\SolutionTemplates** to __C:\Users\Username\Documents\Visual Studio 2019\Templates\ProjectTemplates\Visual C#__

Copy ItemTemplates from **templates\ItemTemplates** to __C:\Users\Username\Documents\Visual Studio 2019\Templates\ItemTemplates\Visual C#__


Create new project by choosing Starnet DDD template.

To add new aggregate you should:
1. Add new StarnetDDD AggregateMessages item to Solution.PL project
2. Add new StarnetDDD Aggregate item to Solution.Domain project
3. Add new StarnetDDD Aggregate Tests item to Solution.Domain.Tests project
4. Replace _DOMAIN_ marker with SolutionName

NCrunch is recommended for live tests. Remember to exclude integration tests.
You will need to provide servicestack licence and NSBus license.xml files as needed.
