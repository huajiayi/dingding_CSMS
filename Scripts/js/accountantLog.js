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

addLoadEvent(function () { fullData(LogName, ContractName, Service, AffirmIncomeGist, AffirmIncomeAmount, InvoiceCount, InvoiceAmount, Cost, worker,Material, Subtotal, GrossrofitMargin) });

//填充数据
function fullData(logName, contractName, service,affirmIncomeGist,AffirmIncomeAmount, subInvoiceCount, subInvoiceAmount, subCost, subworker, subMaterial, subtotal, avgGrossrofitMargin){
	document.getElementById("lbl_logName").innerHTML = logName;
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_service").innerHTML = "服务：" + service;
	document.getElementById("lbl_AffirmIncomeAmount").innerHTML = "确认收入金额：" + AffirmIncomeAmount;
	document.getElementById("lbl_affirmIncomeGist").innerHTML = "确认收入依据：" + affirmIncomeGist;
	document.getElementById("lbl_subInvoiceCount").innerHTML = "已开票数量：" + subInvoiceCount;
	document.getElementById("lbl_subInvoiceAmount").innerHTML = "已开票金额（含税）：" + subInvoiceAmount;
	document.getElementById("lbl_subCost").innerHTML = "已结转成本数量：" + subCost;
	document.getElementById("lbl_subworker").innerHTML = "直接人工：" + subworker;
	document.getElementById("lbl_subMaterial").innerHTML = "直接材料：" + subMaterial;
	document.getElementById("lbl_subtotal").innerHTML = "小计：" + subtotal;
	document.getElementById("lbl_avgGrossrofitMargin").innerHTML = "2017年1-12月毛利率：" + avgGrossrofitMargin;
}

