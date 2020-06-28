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
    $("#person_details").load("/Person/PersonDetailsPartial?id=" + id, function (response, status, xhr) {
        if (status == "success") {
            replaceSVG();
        }
    });
    // window.location.href = "#person_details";
}


/*
* Replace all SVG images with inline SVG
*/
function replaceSVG() {
    jQuery('img.svg').each(function () {
        var $img = jQuery(this);
        var imgID = $img.attr('id');
        var imgClass = $img.attr('class');
        var imgURL = $img.attr('src');
        jQuery.get(imgURL, function (data) {
            // Get the SVG tag, ignore the rest
            var $svg = jQuery(data).find('svg');
            // Add replaced image's ID to the new SVG
            if (typeof imgID !== 'undefined') {
                $svg = $svg.attr('id', imgID);
            }
            // Add replaced image's classes to the new SVG
            if (typeof imgClass !== 'undefined') {
                $svg = $svg.attr('class', imgClass + ' replaced-svg');
            }
            // Remove any invalid XML tags as per http://validator.w3.org
            $svg = $svg.removeAttr('xmlns:a');
            // Check if the viewport is set, if the viewport is not set the SVG wont't scale.
            if ($img.attr('width')) $svg.attr('width', $img.attr('width'))
            if ($img.attr('height')) $svg.attr('height', $img.attr('height'))

            if (!$svg.attr('viewBox') && $svg.attr('height') && $svg.attr('width')) {
                $svg.attr('viewBox', '0 0 ' + $svg.attr('height') + ' ' + $svg.attr('width'))
            }
            // Replace image with new SVG
            $img.replaceWith($svg);
        }, 'xml');
    });
}

$(document).ready(function () {
    replaceSVG();
});