function addLoadEvent(func) {
	var oldonload = window.onload;
	if(typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}
function fullLog(Contract_DataJson) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var serviceArr = JSON.parse(Contract_DataJson.replace(reg, '"'));
    fillService(serviceArr)
}
addLoadEvent(function () { fullLog(Contract_DataJson) });

function fillService(serviceArr){
    var services = document.getElementById("txt_services");
    for (var i = 0; i < serviceArr.length; i++) {
        var service = document.createElement("option");
        service.innerHTML = serviceArr[i].Service;
        service.value = serviceArr[i].ID;
        services.appendChild(service);
	}
}

function typeChange(value){
    var lbl_dateType = document.getElementById("lbl_dateType");
    var datetimepicker = document.getElementById("datetimepicker");
    if (value == "ProjectStart") {
        lbl_dateType.innerHTML = "<span style='color: red;'>*</span>施工日期：";
	    datetimepicker.name = "ProjectStart"
    } else if (value == "DompletedDate") {
        lbl_dateType.innerHTML = "<span style='color: red;'>*</span>竣工日期：";
	    datetimepicker.name = "DompletedDate"
    } else if (value == "DompletedAcceptanceDate") {
	    lbl_dateType.innerHTML = "<span style='color: red;'>*</span>取得竣工验收单日期：";
	    datetimepicker.name = "DompletedAcceptanceDate"
	}
}
