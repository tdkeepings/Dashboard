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
            return false;
        });
    });

    // "X" visibility control
    $(document).on("mouseenter", ".site", function () {
        $(this).find("a.close").show();
    });
    $(document).on("mouseleave", ".site", function () {
        $(this).find("a.close").hide();
    });
    $(document).on("mouseenter", ".column h3", function () {
        $(this).find("a.close").show();
    });
    $(document).on("mouseleave", ".column h3", function () {
        $(this).find("a.close").hide();
    });

    $("input.cancel").each(function () {
        $(this).click(function () {
            overlay().close();
        });
    });

    $("#SiteBgColour").spectrum({
        preferredFormat: 'rgb',
        color: '#069',
        allowEmpty: false,
        clickoutFiresChange: true
    });

    $("#SiteColour").spectrum({
        preferredFormat:'rgb',
        color: '#fff',
        allowEmpty: false,
        clickoutFiresChange: true
    });

    //Keyboard shortcut controller
    $(document).keyup(function (e) {
        //Esc
        if (e.keyCode === 27) {
            overlay().close();
        }

        //Enter
        if (e.keyCode === 13) {
            if (overlay().isVisible()) {
                overlay().accept();
            }
        }
    });
});

//Overlay class, properties (classes, names) mapped to what they are on frontend
function overlay() {
    this.isVisible = function () {
        return $(".overlay").is(":visible");
    };

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

    this.accept = function () {
        $(".overlay").find(".accept").click();
    };

    this.cancel = function () {
        $(".overlay").find(".accept").click();
    };

    return this;
}
