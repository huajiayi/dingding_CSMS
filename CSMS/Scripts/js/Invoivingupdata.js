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
function fullLog() {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var serviceArr = JSON.parse(Contract_DataJson.replace(reg, '"'));
    fillService(serviceArr)
}
addLoadEvent(fullLog);
function fillService(serviceArr) {
    var services = document.getElementById("txt_services");
    for (var i = 0; i < serviceArr.length; i++) {
        var service = document.createElement("option");
        service.innerHTML = serviceArr[i].Service;
        service.value = serviceArr[i].ID;
        services.appendChild(service);
    }
    $("#txt_services").find("option[value=" + ss + "]").attr("selected", true);
}