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

addLoadEvent(function () { fullData(LogName, ContractName, Service, AffirmIncomeGist, AffirmIncomeAmount,  InvoiceAmount, Cost, worker,Material, Subtotal, GrossrofitMargin,AffirmIncomeDate) });

//填充数据
function fullData(logName, contractName, service, affirmIncomeGist, AffirmIncomeAmount, subInvoiceAmount, subCost, subworker, subMaterial, subtotal, avgGrossrofitMargin, AffirmIncomeDate) {
	
    document.getElementById("lbl_logName").innerHTML = logName;
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_service").innerHTML = "服务：" + service;
	if (service.indexOf("更改为") != -1) {
	    $("#lbl_service").css("color", "green");
	}
	document.getElementById("lbl_AffirmIncomeAmount").innerHTML = "确认收入金额：" + AffirmIncomeAmount;
	if (AffirmIncomeAmount.indexOf("更改为") != -1) {
	    $("#lbl_AffirmIncomeAmount").css("color", "green");
	}
	document.getElementById("lbl_affirmIncomeGist").innerHTML = "确认收入依据：" + affirmIncomeGist;
	if (affirmIncomeGist.indexOf("更改为") != -1) {
	    $("#lbl_affirmIncomeGist").css("color", "green");
	}
	document.getElementById("lbl_subCost").innerHTML = "已结转成本数量：" + subCost;
	if (subCost.indexOf("更改为") != -1) {
	    $("#lbl_subCost").css("color", "green");
	}
	document.getElementById("lbl_subworker").innerHTML = "直接人工：" + subworker;
	if (subworker.indexOf("更改为") != -1) {
	    $("#lbl_subworker").css("color", "green");
	}
	document.getElementById("lbl_subMaterial").innerHTML = "直接材料：" + subMaterial;
	if (subMaterial.indexOf("更改为") != -1) {
	    $("#lbl_subMaterial").css("color", "green");
	}
	document.getElementById("lbl_subtotal").innerHTML = "小计：" + subtotal;
	if (subtotal.indexOf("更改为") != -1) {
	    $("#lbl_subtotal").css("color", "green");
	}
	document.getElementById("lbl_avgGrossrofitMargin").innerHTML = "2017年1-12月毛利率：" + avgGrossrofitMargin;
	if (avgGrossrofitMargin.indexOf("更改为") != -1) {
	    $("#lbl_avgGrossrofitMargin").css("color", "green");
	}
	document.getElementById("lbl_AffirmIncomeDate").innerHTML = "确认收入日期：" + AffirmIncomeDate;
	if (AffirmIncomeDate.indexOf("更改为") != -1) {
	    $("#lbl_AffirmIncomeDate").css("color", "green");
	}
}

