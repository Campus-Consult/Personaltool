// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Selectable table row functionality */
$("table.selectable tbody tr").click(function () {
    $("table.selectable tbody tr").removeClass('selectedRow');
    $(this).addClass('selectedRow');
});

/* Load PersonDetailsPartial async */
function loadPersonDetailsPartial(id) {
    $("#person_details").load("/Person/PersonDetailsPartial?id=" + id);
    window.location.href = "#person_details";
}