$(function () {

    $(".addSite").click(function () {
        overlay().open("site");
    });

    $(".addColumn").click(function () {
        overlay().open("column");
    });

    $("a.close").each(function () {
        $(this).click(function () {
            overlay().close();
        });
    });

    $("input.cancel").each(function () {
        $(this).click(function () {
            overlay().close();
        });
    });

    $(document).keyup(function (e) {
        if (e.keyCode == 27) {
            overlay().close();
        }
    });


    //Overlay class, properties (classes, names) mapped to what they are on frontend
    
});

function overlay() {
    this.open = function (type) {
        var isSite = (type === "site") ? true : false;
        var isColumn = (type === "column") ? true : false;

        if (isSite) {
            this.close();
            $(".overlay.siteOverlay").fadeIn(250); 
            $(".overlay.siteOverlay #SiteName").focus();
        } else if (isColumn) {
            this.close();
            $(".overlay.columnOverlay").fadeIn(250);
            $(".overlay.columnOverlay #ColumnName").focus();
        }
    };

    this.clear = function () {
        $(".overlay input[type=text]").each(function () {
            $(this).val("");
        });
    };

    this.close = function () {
        $(".overlay").fadeOut(300);
    };

    return this;
}
