function search() {
    $.ajax({
        type: "GET",
        data: { resource: "people", id: 1 },
        url: location.origin + "/swapi",
        success: function (response) {
            alert(response);
        }
    });
}
var filmDetails = '';
$(document).ready(function () { populateFilmDropdown(); }); 
function populateFilmDropdown() {
    $.ajax({
        type: "GET",
        dataType: "json",
        data: { resource: "films" },
        url: location.origin + "/getresource",
        success: function (response) {
            
            $("#filmdropdown").empty();
             
            if (response.results && Array.isArray(response.results)) {
                filmDetails = response.results;
                response.results.forEach(function (film) {
                    $("#filmdropdown").append(
                        $("<option>").text(film.title).val(film.url)
                    );
                });
            }
        }
    });
}
$(document).ready(function () {
    $("#getstarshipsBtn").click(function () {
        var selectedFilmUrl = $("#filmdropdown").val();
       
        getStarshipsForFilm(selectedFilmUrl);
    });
});
function getStarshipsForFilm(filmUrl) {
    var film = filmDetails.find(function (film) {
        return film.url === filmUrl;
    });
    $("#starshipsList").empty();
    $("#starshipsList").append("<h4> Starships in " + film.title + ":</h4>");
    $("#starshipsList").append("<ul>");
    for (var i = 0; i < film.starships.length; i++) {
        var starshipUrl = film.starships[i];
        var urlSegments = starshipUrl.split('/');
        var resourceId = urlSegments[urlSegments.length - 2];
        var resourceName = urlSegments[urlSegments.length - 3]; 
        $.ajax({
            type: "GET",
            data: { resource: resourceName, id: resourceId },
            url: location.origin + "/swapi",
            success: function (response) {
                var jsonResponse = JSON.parse(response);
                console.log(response);
                if (response) {
                    $("#starshipsList").append("<li>" + jsonResponse.name + "</li>");
                }
            }
        }); 
    }
    $("#starshipsList").append("</ul>");
}