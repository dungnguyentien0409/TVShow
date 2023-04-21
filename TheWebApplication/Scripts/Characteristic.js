var preUrl = '/Home/'

$(".addNew").click(function (e) {
    var $buttonClicked = $(this);
    var id = $buttonClicked.attr('data-id');
    var options = { "backdrop": "static", keyboard: true };
    e.stopPropagation();
    $.ajax({
        type: "GET",
        url: preUrl + "AddNew",
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

    var data = $("#CreateCharacteristic").serialize();
    console.log(data);
    
    $.ajax({
        type: "POST",
        url: preUrl + "CreateCharacteristic",
        data: $("#CreateCharacteristic").serialize(),
        datatype: "json",
        success: function (data) {
            if (data == true) {
                $('#myModal').modal('hide');
                alert("New characteristic has been created!")
            }
            else {
                alert("Error happened when inserting new characteristic");
            }
        },
        error: function () {
            alert("Error happened when inserting new characteristic");
        }
    });
}