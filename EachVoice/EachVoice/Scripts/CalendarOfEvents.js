var calendar = new DayPilot.Calendar("calendar");
calendar.viewType = "Week";
calendar.init();
var e = new DayPilot.Event({ start: new DayPilot.Date(), end: (new DayPilot.Date()).addHours(5), value: DayPilot.guid(), text: "New Event", resource: 'E' });
calendar.events.add(e);
calendar.eventMoveHandling = "Disabled";
calendar.eventResizeHandling = "Disabled";

//this is the api documentation: https://api.daypilot.org/daypilot-event-data/