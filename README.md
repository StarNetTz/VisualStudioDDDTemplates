# VisualStudioDDDTemplates
Starnet DDD templates for VS 2019
## Usage
Copy StarnetDDD.zip from:
  templates\SolutionTemplates
to
  C:\Users\*Username*\Documents\Visual Studio 2019\Templates\ProjectTemplates\Visual C#

Install ItemTemplates by copying them from:
  templates\ItemTemplates
to
  C:\Users\*Username*\Documents\Visual Studio 2019\Templates\ItemTemplates\Visual C#


Create new project by choosing Starnet DDD template.

Add new aggregate by following these steps:
1. Add new StarnetDDD AggregateMessages item to Solution.PL project
2. Add new StarnetDDD Aggregate item to Solution.Domain project
3. Add new StarnetDDD Aggregate Tests item to Solution.Domain.Tests project
4. Replace _DOMAIN_ marker with SolutionName

NCrunch is recommended for live tests. Remember to exclude integration tests.
You will need to provide servicestack licence and NSBus license.xml files as needed.
