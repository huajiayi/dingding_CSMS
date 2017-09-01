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

function insertAfter(newElement, targetElement) {
    var parent = targetElement.parentNode;
    if (parent.lastChild == targetElement) {
        parent.appendChild(newElement);
    } else {
        parent.insertBefore(newElement, targetElement.nextSibling);
    }
}

addLoadEvent(function () { fullData(ContractName, Contract_Amount, AmountCollection, NoAmountCollection) });
addLoadEvent(function () { fullLog(SalesLogJson) });
//addLoadEvent(function () { hasLog(SalesLogJson) });
//判断有没有日志，没有则显示尚无日志
//function hasLog(haslog) {
   
//    var log = document.getElementById("log");
//    if (haslog=="[]") {
//        log.innerHTML = "尚无日志";
//    } else {
//        var logList = document.createElement("section");
//        logList.id = "cd-timeline";
//        logList.className = "cd-container";
//        insertAfter(logList, log);
//    }
//}

//填充数据
function fullData(contractName, contractAmount, amountReceived, amountNotReceived){
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_contractAmount").innerHTML = "总金额：" + contractAmount;
	document.getElementById("lbl_amountReceived").innerHTML = "已收金额：" + amountReceived;
	document.getElementById("lbl_amountNotReceived").innerHTML = "未收金额：" + amountNotReceived;
}

//填充日志ddddd
function fullLog(haslog) {
    var log = document.getElementById("log");
    if (haslog == "[]") {
        log.innerHTML = "尚无日志";
    } else {
        var logList = document.createElement("section");
        logList.id = "cd-timeline";
        logList.className = "cd-container";
        insertAfter(logList, log);
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
        var SalesLog = JSON.parse(SalesLogJson.replace(reg, '"'));
        for (var i = 0; i < SalesLog.length; i++) {
            addLog(SalesLog[i].LogName, SalesLog[i].Service, SalesLog[i].AffirmIncomeAmount, SalesLog[i].AffirmIncomeDate, SalesLog[i].LogDate, SalesLog[i].Name);
        }
    }
}


//添加日志
function addLog(logName, service, log, date, logDate, name) {
    var logList = document.getElementById("cd-timeline");
    var div_block = document.createElement("div");
    div_block.className = "cd-timeline-block";
    var div_img = document.createElement("div");
    div_img.className = "cd-timeline-img cd-picture";
    var img = document.createElement("img");
    img.src = "../../Content/img/cd-icon-picture.svg";
    div_img.appendChild(img);
    div_block.appendChild(div_img);
    var div_content = document.createElement("div");
    div_content.className = "cd-timeline-content";
    var h2 = document.createElement("h2");
    h2.innerHTML = logName;
    var h3_service = document.createElement("h3");
    h3_service.innerHTML = service;
    var h3 = document.createElement("h3");
    h3.innerHTML = "收款 " + log + " 元";
    var h3_date = document.createElement("h3");
    h3_date.innerHTML = "收款日期：<br/>" + date;
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = logDate + " by " + name;
    div_content.appendChild(h2);
    div_content.appendChild(h3_service)
    div_content.appendChild(h3);
    div_content.appendChild(h3_date);
    div_content.appendChild(span);
    div_block.appendChild(div_content);
    logList.appendChild(div_block);
}
