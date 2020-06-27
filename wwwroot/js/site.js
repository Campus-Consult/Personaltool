// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Selectable table row functionality */
$("table.table-selectable tbody tr").click(function () {
    $("table.table-selectable tbody tr").removeClass('selectedRow');
    $(this).addClass('selectedRow');
});


/* Filters a searchable table based on search-input */
$(document).ready(function () {
    $("#search-input").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("table.table-searchable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});


/* Load PersonDetailsPartial async */
function loadPersonDetailsPartial(id) {
    $("#person_details").load("/Person/PersonDetailsPartial?id=" + id);
    // window.location.href = "#person_details";
}