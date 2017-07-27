var comcalendar = new DayPilot.Calendar("calendar");
comcalendar.viewType = "Week";
comcalendar.init();
comcalendar.eventMoveHandling = "Disabled";
comcalendar.eventResizeHandling = "Disabled";

function addEvent()
{
    var current = new Date();
    var year = (current.getUTCFullYear()).toString();
    var month = pad(current.getUTCMonth() + 1);
    var startDay = stringPad(document.getElementById('startDay').value);
    var startHour = stringPad(document.getElementById('startHour').value);
    var startMin = stringPad(document.getElementById('startMin').value);
    var endDay = stringPad(document.getElementById('endDay').value);
    var endHour = stringPad(document.getElementById('endHour').value);
    var endMin = stringPad(document.getElementById('endMin').value);
    var eventTitle = document.getElementById('eventTitle').value;

    if (eventTitle == '' || eventTitle == null)
    {

    }
    else
    {
        var e = new DayPilot.Event(
            {
                //Format for time is "YYYY-MM-DDTHH:MM:SS
                start: new DayPilot.Date(year + "-" + month + "-" + startDay + "T" + startHour + ":" + startMin + ":00"),
                end: new DayPilot.Date(year + "-" + month + "-" + endDay + "T" + endHour + ":" + endMin + ":00"),
                value: DayPilot.guid(),
                text: eventTitle + "<br/>" + startDay + ", " + startHour + ":" + startMin + " -<br>" + endDay + ", " + endHour + ":" + endMin,
                resource: 'E'
            });

        comcalendar.events.add(e);
        document.getElementById('startDay').value = '';
        document.getElementById('startHour').value = '';
        document.getElementById('startMin').value = '';
        document.getElementById('endDay').value = '';
        document.getElementById('endHour').value = '';
        document.getElementById('endMin').value = '';
        document.getElementById('eventTitle').value = '';
    }
}

function pad(num)
{
    if (num < 10) return "0" + num;
    return num;
}

function stringPad(time)
{
    var int = parseInt(time);
    return pad(int);
}

//this is the api documentation: https://api.daypilot.org/daypilot-event-data/