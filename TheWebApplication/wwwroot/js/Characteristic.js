var preUrl = '/characteristic-management'
var pageIndex = 0;
var locationId = "00000000-0000-0000-0000-000000000000";

$("#addNew").click(function (e) {
    var $buttonClicked = $(this);
    var id = $buttonClicked.attr('data-id');
    var options = { "backdrop": "static", keyboard: true };
    e.stopPropagation();
    $.ajax({
        type: "GET",
        url: preUrl + "/characteristic",
        contentType: "application/json; charset=utf-8",
        data: { "Id": id },
        datatype: "json",
        success: function (data) {
            $('#myModalContent').html(data);
            displayRequiredLabel();
            $('#myModal').modal(options);
            $('#myModal').modal('show');
        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
});

$("body").click(function (e) {
    if ($(e.target).closest('#myModalContent').length === 0) {
        $('#myModal').modal('hide')
    }
});

function selectLocation(selectedLocationId) {
    locationId = selectedLocationId;

    pageIndex = 0;
    $("#TotalPage").val(0);
    loadCharGrid(0);
}

function movePage(direction) {
    totalPage = $("#TotalPage").val();
    pageIndex += direction;

    if (pageIndex == 0 && direction == 0) {
        //init
    }
    else if (pageIndex < 0) {
        pageIndex = 0;
        return;
    }
    else if (pageIndex >= totalPage) {
        pageIndex = totalPage - 1;
        return;
    }

    loadCharGrid(pageIndex);
}

function loadCharGrid(pageIndex) {
    var criteria = {
        "LocationId": locationId != 'null' ? locationId : null,
        "PageIndex": pageIndex
    };

    $.ajax({
        type: "POST",
        datatype: "json",
        url: preUrl + "/characteristics",
        data: criteria,
        success: function (data) {
            $('#charGrid').html(data);
        },
        error: function (e) {
            debugger;
            alert("Dynamic content load failed.");
        }
    });
}

function displayRequiredLabel() {
    $('input[type=text]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0) {
                label.append('<span style="color:red"> *</span>');
            }
        }
    });
}

function validateRequiredField() {
    var check = true;
    $('input[type=text]').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var text = $(this).val();

            if (text == "") {
                $(this).val('').css("border-color", "red");
                check = false;
            }
        }
    });

    return check;
}

function submitDetailsForm() {
    var check = validateRequiredField();

    if (!check) return;

    $.ajax({
        type: "POST",
        url: preUrl + "/characteristic",
        data: $("#CreateCharacteristic").serialize(),
        success: function (data) {
            if (data == true) {
                $('#myModal').modal('hide');
                alert("New characteristic has been created!")

                movePage(0);
                pageIndex = 0;
            }
            else {
                alert("Error happened when inserting new characteristic");
            }
        },
        error: function (e) {
            alert("Error happened when inserting new characteristic");
        }
    });
}

function getFormData(formId) {
    var unindexed_array = $(formId).serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}