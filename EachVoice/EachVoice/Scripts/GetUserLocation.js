$(document).ready(getLocation());

//location logic occurs here, this now uses general location dom functions that should be compatible with all browsers
var x = document.getElementById('location');
var lat = 1;
var long = 1;
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}
function showPosition(position) {
    lat = position.coords.latitude;
    long = position.coords.longitude;
}

//on button click we call the API and show representatives
$(document).ready(
    $('#geolocation').click(function () {
        $('#reps').show();
        $('#geolocation').hide();
        var url = 'https://openstates.org/api/v1/legislators/geo/?lat=' + lat + "&long=" + long;
        $.getJSON("https://openstates.org/api/v1/legislators/geo/?lat=" + lat + "&long=" + long, function (data) {
            console.log(lat + ', ' + long);
            console.log(data);
            $.each(data,
                function () {
                    if (this.chamber == "upper") {
                        $('#reps').append('<li><b>' + this.full_name + '</b>' + ": " + this.party + ", Oregon Senate" + '</li>');
                    }
                    else {
                        $('#reps').append('<li><b>' + this.full_name + '</b>' + ": " + this.party + ",  Oregon House of Representatives" + '</li>');
                    }
                });
        });
}));

