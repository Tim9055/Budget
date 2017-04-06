$(function () {
    //This cancels the edit
    $(document).keyup(function (e) {
        if (e.keyCode === 27) {
            var $input = $(".clickToEdit").find("input");
            var $span = $input.siblings("span");
            $input.text($span.text());
            $span.show();
            $input.remove();
        }
    });
    $(".clickToEdit").on("click", "tr td", function () {
        var $td = $(this);
        var billId = $td.data("bill-id");
        var billItem = $td.data("bill-item");
        if (billId !== undefined) {
            var billItemText = $td.children("span").text();
            if ($td.children("input").length === 0) {
                $td.children("span").hide();
                if ($td.data("bill-item") === "DateDue" || $td.data("bill-item") === "DatePaid") {
                    $td.append('<input type="text" data-bill-id="' + billId + '" data-bill-item="' + billItem + '" value="' + billItemText + '" class="datepicker form-control" data-date-format="mm/dd/yyyy" />');
                    $(".datepicker").datepicker();
                }
                else {
                    $td.append('<input type="text" data-bill-id="' + billId + '" data-bill-item="' + billItem + '" value="' + billItemText + '" class="form-control" />');
                }
                $td.children("input").focus().select();
            }
        }
        else {
        }
    });
    $(document).keyup(function (e) {
        if (e.keyCode === 13) {
            var $input = $(".clickToEdit").find("input");
            //var $span = $input.siblings("span");
            //$input.text($span.text());
            //$span.show();
            //$input.remove();
            var billItemText = $input.val();
            var billId = $input.data("bill-id");
            var billItem = $input.data("bill-item");
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
var ajaxCall = function (billId, billItem, billItemText) {
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
//# sourceMappingURL=index.js.map