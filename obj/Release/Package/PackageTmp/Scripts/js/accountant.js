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
	if(parent.lastChild == targetElement) {
		parent.appendChild(newElement);
	} else {
		parent.insertBefore(newElement, targetElement.nextSibling);
	}
}

var N = 0;
var Accountant = new Array();
addLoadEvent(function () { fillServiceZZ(AccountantJson, AccountantLogJson) });
//addLoadEvent(function () { fillService(Accountant) });
//addLoadEvent(function () { fullData(ContractName, Contract_Amount, SubAffirmIncomeAmount, NoAmountCollection, Accountant.AffirmIncomeGist, Accountant.AffirmIncomeAmount, Accountant.SubInvoiceCount, Accountant.SubInvoiceAmount, Accountant.SubCost, Accountant.Subworker, Accountant.SubMaterial, Accountant.Subtotal, Accountant.AvgGrossrofitMargin) });


function fillServiceZZ(AccountantJson) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象   LogDatesJson 
    Accountant = JSON.parse(AccountantJson.replace(reg, '"'));
   
    fullData(ContractName, Contract_Amount, SubAffirmIncomeAmount, NoAmountCollection, Accountant[0].AffirmIncomeGist, Accountant[0].SubAffirmIncomeAmount, Accountant[0].SubInvoiceCount, Accountant[0].SubInvoiceAmount, Accountant[0].SubCost, Accountant[0].Subworker, Accountant[0].SubMaterial, Accountant[0].Subtotal, Accountant[0].AvgGrossrofitMargin);
    fillService(Accountant);
    fullLog(AccountantLogJson)
}
//填充数据
function fullData(contractName, amount, subAffirmIncomeAmount, noAmountCollection, affirmIncomeGist, AffirmIncomeAmount, subInvoiceCount, subInvoiceAmount, subCost, subworker, subMaterial, subtotal, avgGrossrofitMargin){
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_amount").innerHTML = "总金额：" + amount;
	document.getElementById("lbl_subAffirmIncomeAmount").innerHTML = "已收金额：" + subAffirmIncomeAmount;
	document.getElementById("lbl_noAmountCollection").innerHTML = "未收金额：" + noAmountCollection;
	document.getElementById("lbl_affirmIncomeGist").innerHTML = "确认收入依据：" + affirmIncomeGist;
	document.getElementById("lbl_AffirmIncomeAmount").innerHTML = "确认收入金额（不含税）：" + AffirmIncomeAmount;
	document.getElementById("lbl_subInvoiceCount").innerHTML = "已开票数量：" + subInvoiceCount;
	document.getElementById("lbl_subInvoiceAmount").innerHTML = "已开票金额（含税）：" + subInvoiceAmount;
	document.getElementById("lbl_subCost").innerHTML = "已结转成本数量：" + subCost;
	document.getElementById("lbl_subworker").innerHTML = "直接人工：" + subworker;
	document.getElementById("lbl_subMaterial").innerHTML = "直接材料：" + subMaterial;
	document.getElementById("lbl_subtotal").innerHTML = "小计：" + subtotal;
	document.getElementById("lbl_avgGrossrofitMargin").innerHTML = "2017年1-12月毛利率：" + avgGrossrofitMargin;
}

//填充日志
function fullLog(haslog) {
	var log = document.getElementById("log");
	if(haslog == "[]") {
	    log.innerHTML = "尚无日志";
	    $("#log").next().remove();
	} else {
		var logList = document.createElement("section");
		logList.id = "cd-timeline";
		logList.className = "cd-container";
		insertAfter(logList, log);
		var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
		var AccountantLog = JSON.parse(haslog.replace(reg, '"'));
		
		if (AccountantLog.length < 5) {
		    $("#more").remove();
		}
		for (var i = 0; i < AccountantLog.length; i++) {
		    addLog(AccountantLog[i].LogName, AccountantLog[i].LogDate, AccountantLog[i].Service, AccountantLog[i].Name, AccountantLog[i].ID);
		}
	}
}

//添加日志（加了个服务）
function addLog(logName,date,service,name,ID) {
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
    var h3 = document.createElement("h3");
    h3.innerHTML = "结算原因：" + logName;
    var p_service = document.createElement("p");
    p_service.innerHTML = "服务款项：" + service;
    var p_log = document.createElement("p");
    p_log.innerHTML = "做了1次修改";
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = date + " by " + name;
    var a = document.createElement("a");
    a.className = "cd-read-more";
    a.href = " AccountantLog?ID="+ID;
    a.innerHTML = "详情"
    div_content.appendChild(h3);
    div_content.appendChild(p_service)
    div_content.appendChild(a);
    div_content.appendChild(span);
    div_block.appendChild(div_content);
    logList.appendChild(div_block);
}


function fillService(Accountant) {
	var services = document.getElementById("txt_services");
	for (var i = 0; i < Accountant.length; i++) {
		var service = document.createElement("option");
		service.innerHTML = Accountant[i].Service;
		service.value = Accountant[i].Service;
		services.appendChild(service);
	}
}

function serviceChanged(service) {
    for (var i = 0; i < Accountant.length; i++) {
        if (Accountant[i].Service == service) {
            fullServiceData(Accountant[i]);
        }
    }
}

//填充每个服务的数据
function fullServiceData(Accountant) {
	document.getElementById("lbl_affirmIncomeGist").innerHTML = "确认收入依据：" + Accountant.AffirmIncomeGist;
	document.getElementById("lbl_AffirmIncomeAmount").innerHTML = "确认收入金额（不含税）：" + Accountant.SubAffirmIncomeAmount;
	document.getElementById("lbl_subInvoiceCount").innerHTML = "已开票数量：" + Accountant.SubInvoiceCount;
	document.getElementById("lbl_subInvoiceAmount").innerHTML = "已开票金额（含税）：" + Accountant.SubInvoiceAmount;
	document.getElementById("lbl_subCost").innerHTML = "已结转成本数量：" + Accountant.SubCost;
	document.getElementById("lbl_subworker").innerHTML = "直接人工：" + Accountant.Subworker;
	document.getElementById("lbl_subMaterial").innerHTML = "直接材料：" + Accountant.SubMaterial;
	document.getElementById("lbl_subtotal").innerHTML = "小计：" + Accountant.Subtotal;
	document.getElementById("lbl_avgGrossrofitMargin").innerHTML = "2017年1-12月毛利率：" + Accountant.AvgGrossrofitMargin;
}

function lazyLoad() {
    N = N + 5;
    $.ajax({
        type: "POST",
        url: "/Accountant/AccountantLogAjaxTT?ID=" + N,
        async: true,
        cache: false,
        success: function (data) {
            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
            var AccountantLog = JSON.parse(data.replace(reg, '"'));
           
            if (AccountantLog.length < 5) {
                $("#more").remove();
            }
            if (data == "[]") {
                N -= 5;
                alert("暂无最新数据！");
                return false;
            }
            for (var i = 0; i < AccountantLog.length; i++) {
                if (!document.getElementById("cd-timeline")) {
                    return false;
                }
                addLog(AccountantLog[i].LogName, AccountantLog[i].LogDate, AccountantLog[i].Service, AccountantLog[i].Name, AccountantLog[i].ID);
            }
        },
        error: function () {
            alert("请求失败");
        }
    });
    var $timeline_block = $('.cd-timeline-block');
    //hide timeline blocks which are outside the viewport
    $timeline_block.each(function () {
        if ($(this).offset().top > $(window).scrollTop() + $(window).height() * 0.75) {
            $(this).find('.cd-timeline-img, .cd-timeline-content').addClass('is-hidden');
        }
    });
    //on scolling, show/animate timeline blocks when enter the viewport
    $(window).on('scroll', function () {
        $timeline_block.each(function () {
            if ($(this).offset().top <= $(window).scrollTop() + $(window).height() * 0.75 && $(this).find('.cd-timeline-img').hasClass('is-hidden')) {
                $(this).find('.cd-timeline-img, .cd-timeline-content').removeClass('is-hidden').addClass('bounce-in');
            }
        });
    });
}