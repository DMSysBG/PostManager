
function dmDatepicker(htmlItem) {
    $(htmlItem).datepicker({
        dateFormat: "dd.mm.yy"
    });
}

function dmAutocomplete(sourceUrl, itemName, itemId) {
    $(itemName).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: sourceUrl,
                data: "{'term':'" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        minLength: 0,
        select: function (event, ui) {
        },
        change: function (event, ui) {
            console.log(ui.item);
            if (ui.item == null) {
                $(itemId).val('');
            }
            else {
                $(itemId).val(ui.item.id);
            }
        },
        messages: {
            noResults: '',
            results: function () { }
        }
    }).focus(function () {
        $(this).autocomplete("search");
    });
}