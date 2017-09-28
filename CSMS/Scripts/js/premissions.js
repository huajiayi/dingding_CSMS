$(function () {

    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var premission = JSON.parse(UserJson.replace(reg, '"'));
    var Production_p = premission.Production_p;
    var Warehouse_p = premission.Warehouse_p;
    var Project_p = premission.Project_p;
    var Sales_p = premission.Sales_p;
    var Invoicings_p = premission.Invoicings_p;
    var Accountant_p = premission.Accountant_p;
    var Summation_p = premission.Summation_p;
   
    if (Production_p == 0) {
        $("#menu a:contains(生产)").attr("href", "#")
           .click(function () {
               return false;
           }).html("生产（无权限）")
           .css("color", "gray");
    }
    if (Warehouse_p == 0) {
        $("#menu a:contains(发货)").attr("href", "#")
            .click(function () {
                return false;
            }).html("发货（无权限）")
            .css("color", "gray");
    }
    if (Project_p == 0) {
        $("#menu a:contains(施工)").attr("href", "#")
            .click(function () {
                return false;
            }).html("施工（无权限）")
            .css("color", "gray");
    }
    if (Sales_p == 0) {
        $("#menu a:contains(收款)").attr("href", "#")
            .click(function () {
                return false;
            }).html("收款（无权限）")
            .css("color", "gray");
    }
    if (Invoicings_p == 0) {
        $("#menu a:contains(开票)").attr("href", "#")
            .click(function () {
                return false;
            }).html("开票（无权限）")
            .css("color", "gray");
    }
    if (Accountant_p == 0) {
        $("#menu a:contains(结算)").attr("href", "#")
            .click(function () {
                return false;
            }).html("结算（无权限）")
            .css("color", "gray");
    }
    if (Summation_p == 0) {
        $("#menu a:contains(合同汇总)").attr("href", "#")
            .click(function () {
                return false;
            }).html("合同汇总（无权限）")
            .css("color", "gray");
    }

});
function show(p) {
    if (DingTalkPC) {

        DingTalkPC.device.notification.alert({
            message: p,
            title: "提示",//可传空
            buttonName: "确定",
            onSuccess: function () {
                /*回调*/

            },
            onFail: function (err) { }
        });

    }
    if (dd) {
        dd.ready(function () {
            dd.device.notification.alert({
                message: p,
                title: "提示",//可传空
                buttonName: "确定",
                onSuccess: function () {

                },
                onFail: function (err) { }
            });
        });
    }
}

//function premission() {

//}