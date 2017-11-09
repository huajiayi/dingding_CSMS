function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            oldonload();
            func();
        }
    }
}
var Accountants
function fullLog() {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var serviceArr = JSON.parse(Contract_DataJson.replace(reg, '"'));
    fillService(serviceArr)
}
function addLog() {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    Accountants = JSON.parse(AccountantJson.replace(reg, '"'));
    for (var i = 0; i < Accountants.length; i++) {
        if (Accountants[i].Service == Service) {

            DoLog(Accountants[i]);
        }
    }

}
addLoadEvent(fullLog);
addLoadEvent(addLog);
function fillService(serviceArr) {
    var services = document.getElementById("txt_services");
    for (var i = 0; i < serviceArr.length; i++) {
        var service = document.createElement("option");
        service.innerHTML = serviceArr[i].Service;
        service.value = serviceArr[i].ID;
        services.appendChild(service);
    }
}
function DoLog(Accountant) {
    var as
    if (Accountant.AffirmIncomeDate == null || Accountants.AffirmIncomeDate=="") {
      
        $('.datepicker').pickadate({
            format: 'yyyy-mm-dd'
        });
        as= new Date().Format("yyyy-MM-dd");
     
       
    } else {
        if (Accountant.AffirmIncomeDate.indexOf(" 0:00:00") != -1) {
            as = Accountant.AffirmIncomeDate.replace(' 0:00:00', '').replace('/', '-').replace('/', '-');
        }
        if (Accountant.AffirmIncomeDate.indexOf(' 12:00:00 AM') != -1) {
            as = Accountant.AffirmIncomeDate.replace(' 12:00:00 AM', '').replace('/', '-').replace('/', '-');
        }
    }

    $("#txt_services").find("option[value=" + Accountant.ServiceID + "]").attr("selected", true);
    $("#txt_contract_Type").find("option[value=" + Accountant.AffirmIncomeGist + "]").attr("selected", true);
    $("#txt_affirmIncomeAmount").val(Accountant.SubAffirmIncomeAmount);
    $("#txt_cost").val(Accountant.SubCost);
    $("#txt_worker").val(Accountant.Subworker);
    $("#txt_material").val(Accountant.SubMaterial);
    $("#txt_affirmIncomeDate").val(as);
    //var grossrofitMargin = (parseFloat(Accountant.SubAffirmIncomeAmount) - (parseFloat(Accountant.Subworker) + parseFloat(Accountant.SubMaterial))) / parseFloat(Accountant.SubAffirmIncomeAmount)
    //grossrofitMargin = grossrofitMargin.toFixed(2)
    $("#txt_grossrofitMargin").val(Accountant.AvgGrossrofitMargin);
}
$("#txt_services").change(function () {
    for (var i = 0; i < Accountants.length; i++) {
        if (Accountants[i].Service == $("#txt_services").find("option:selected").text()) {
            DoLog(Accountants[i]);
        }
    }
});
function computations() {
    if ($("#txt_affirmIncomeAmount").val()=="") {
        $("#txt_affirmIncomeAmount").val(0);
    }
    if ($("#txt_worker").val()=="") {
        $("#txt_worker").val(0);
    }
    if ($("#txt_material").val() == "") {
        $("#txt_material").val(0);
    }
    
    var grossrofitMargin = (parseFloat($("#txt_affirmIncomeAmount").val()) - (parseFloat($("#txt_worker").val()) + parseFloat($("#txt_material").val()))) / parseFloat($("#txt_affirmIncomeAmount").val())
    grossrofitMargin = grossrofitMargin.toFixed(2)
    $("#txt_grossrofitMargin").val(grossrofitMargin);
}
