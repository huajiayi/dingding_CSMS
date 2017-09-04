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

addLoadEvent(function () { fullData(LogName, ContractName, Service, ProjectStart, DompletedDate, DompletedAcceptanceDate) });

//填充数据
function fullData(logName, contractName, services, projectStart, pompletedDate, pompletedAcceptanceDate){
    document.getElementById("lbl_logName").innerHTML = logName;
    document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
    document.getElementById("lbl_services").innerHTML = "服务款项：" + services;
    if (projectStart != "") {
        document.getElementById("lbl_projectStart").innerHTML = "施工日期：" + projectStart;
    } 
    else if (pompletedDate != "") {
        document.getElementById("lbl_pompletedDate").innerHTML = "竣工日期：" + pompletedDate;
    }
    else {
        document.getElementById("lbl_pompletedAcceptanceDate").innerHTML = "取得竣工验收单日期：" + pompletedAcceptanceDate;
    }
    console.log(projectStart + "," + pompletedDate + "," + pompletedAcceptanceDate);
}

