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





addLoadEvent(function () { fullData(ContractName, Customer, Contract_Type, Contract_Amount, Count, Contract_Number, Contract_Date) });
addLoadEvent(function(){fullServices(ss)});

//填充数据
function fullData(contractName, costumerName, contractType, contractAmount, count, contractNumber, signatureDate){
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_costumerName").innerHTML = "合同方客户名称：" + costumerName;
	document.getElementById("lbl_contractType").innerHTML = "合同类别：" + contractType;
	document.getElementById("lbl_contractAmount").innerHTML = "合同金额（人民币元）：" + contractAmount;
	document.getElementById("lbl_count").innerHTML = "数量（套/个）：" + count;
	document.getElementById("lbl_contractNumber").innerHTML = "合同编号：" + contractNumber;
	document.getElementById("lbl_signatureDate").innerHTML = "合同签署日期：" + signatureDate;
}

//填充服务款项
function fullServices(ss) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var serviceArr = JSON.parse(ss.replace(reg, '"'));
	var serviceList = document.getElementById("serviceList");
	for(var i = 1; i <= serviceArr.length; i++){
		var tr = document.createElement("tr");
		var td = document.createElement("td");
		td.innerHTML = serviceArr[i - 1];
		tr.appendChild(td);
		serviceList.appendChild(tr);
	}
}


window.confirm = function (message) {
    var iframe = document.createElement("IFRAME");
    iframe.style.display = "none";
    iframe.setAttribute("src", 'data:text/plain,');
    document.documentElement.appendChild(iframe);
    var alertFrame = window.frames[0];
    var result = alertFrame.window.confirm(message);
    iframe.parentNode.removeChild(iframe);
    return result;
};

function deleteContract() {
    var deleteContract = confirm("确定要删除此合同吗？");
    if (deleteContract) {
        location.href = "deleteContract";
    }
}
