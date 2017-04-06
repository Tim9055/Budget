interface JQuery {
    datepicker(): void;
    datepicker(param): void;
}

$(function () {
    //This cancels the edit
    $(document).keyup(function (e) {
        if (e.keyCode === 27) { // escape key maps to keycode `27`
            var $input = $(".clickToEdit").find("input");
            var $span = $input.siblings("span");
            $input.text($span.text());
            $span.show();
            $input.remove();
        }
    });

    $(".clickToEdit").on("click","tr td", function() {
        var $td = $(this);
        let billId: number = $td.data("bill-id");
        let billItem: string = $td.data("bill-item");
        if (billId !== undefined) {
            let billItemText: string = $td.children("span").text();
            if ($td.children("input").length === 0) {
                $td.children("span").hide();
                if ($td.data("bill-item") === "DateDue" || $td.data("bill-item") === "DatePaid") {
                    $td.append('<input type="text" data-bill-id="' + billId + '" data-bill-item="' + billItem + '" value="' + billItemText + '" class="datepicker form-control" data-date-format="mm/dd/yyyy" />');
                    $(".datepicker").datepicker();
                } else {
                    $td.append('<input type="text" data-bill-id="' + billId + '" data-bill-item="' + billItem + '" value="' + billItemText + '" class="form-control" />');
                }
                
                $td.children("input").focus().select();
            }

        } else {
            //console.log("Not a number");
        }
        
    });

    $(document).keyup(function (e) {
        if (e.keyCode === 13) { // enter key maps to keycode `13`
            var $input = $(".clickToEdit").find("input");
            //var $span = $input.siblings("span");
            //$input.text($span.text());
            //$span.show();
            //$input.remove();
            let billItemText: string = $input.val();
            let billId: number = $input.data("bill-id");
            let billItem: string = $input.data("bill-item");
            $(".datepicker").datepicker("destroy");
            // Initiate the request!
            ajaxCall(billId, billItem, billItemText);
        }
    });
    //$(".clickToEdit").on("blur", "tr td input", function() {
    //    var $input = $(this);
    //    let billItemText: string = $input.val();
    //    let billId: number = $input.data("bill-id");
    //    let billItem: string = $input.data("bill-item");
    //    // Initiate the request!
    //    ajaxCall(billId,billItem,billItemText);
    //    //$input.siblings("span").text(billItemText).show();
    //    //$input.remove();
    //});
    $("#btnPopulateBills").click(PopulateBills);
});

// Create the "callback" functions that will be invoked when...

// ... the AJAX request is successful
var updatePage = function (resp) {
    $("#tableData").html(resp);
};

// ... the AJAX request fails
var printError = function (req, status, err) {
    console.log('something went wrong', status, err);
};

// Create an object to describe the AJAX request
var ajaxCall = function(billId, billItem, billItemText) {
    var ajaxOptions = {
        url: "/bills/update",
        type: "POST",
        //dataType: "json",
        data: {
            billId: billId,
            billItem: billItem,
            billItemText: billItemText
        },
        success: updatePage,
        error: printError
    };
    $.ajax(ajaxOptions);
};

// Create an object to describe the AJAX request
var PopulateBills = function () {
    var monthYear = $("#searchMonthYear").val();
    var ajaxOptions = {
        url: "/bills/populateBills",
        type: "POST",
        //dataType: "json",
        data: {
            monthYear: monthYear
        },
        success: updatePage,
        error: printError
    };
    $.ajax(ajaxOptions);
};