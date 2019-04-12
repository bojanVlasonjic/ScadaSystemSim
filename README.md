# ScadaSystemSim
A wcf application simulating a scada system. It is derived into 6 projects:
- Alarm Display - console application used for logging the alarms that occured  to the console.
- Database manager - wpf application used for CRUD operations on Tags(entities in the scada system which are tracked and displayed).
- Scada model - a class library containing the model of the system. It is included in all other projects.
- Service - the wcf application that communicates with the rest of the projects and memorizes Tags.
- Trending - windows forms application for displaying the tag values.
- Real time unit - console application simulating a unit which measures values and stores them into the Real time driver. It generates random numbers and stores them in the selected address, which the tags can access.

Note: this was a college project, the detailed specification is in the SNUS-projekat.pdf file. The specification is written in Serbian language.
