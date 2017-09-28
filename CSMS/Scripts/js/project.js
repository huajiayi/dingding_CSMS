/// <reference path="../jquery.validate.min.js" />
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
addLoadEvent(function () { fullServiceData(Project_dataJson, ContractName) });
addLoadEvent(function() {
    fullLog(ProjectLogJson)
});

function fullServiceData(Project_dataJson, ContractName) {
    document.getElementById("lbl_contractName").innerHTML = "合同名称：" + ContractName;
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var Project_data = JSON.parse(Project_dataJson.replace(reg, '"'));
    for (var i = 0; i < Project_data.length; i++) {
        fullData(Project_data[i].Service, Project_data[i].ProjectStart, Project_data[i].DompletedDate, Project_data[i].DompletedAcceptanceDate);
    }
}

//填充服务数据
function fullData(services, projectStart, pompletedDate, pompletedAcceptanceDate){
	
	var div = document.createElement("div");
	div.className = "service";
	var lbl_services = document.createElement("label");
	lbl_services.className = "brlabel-lg text-center";
	lbl_services.innerHTML = services;
	var lbl_projectStart = document.createElement("label");
	lbl_projectStart.className = "brlabel-lg";
	lbl_projectStart.innerHTML = "施工日期：" + projectStart;
	var lbl_pompletedDate = document.createElement("label");
	lbl_pompletedDate.className = "brlabel-lg";
	lbl_pompletedDate.innerHTML = "竣工日期：" + pompletedDate;
	var lbl_pompletedAcceptanceDate = document.createElement("label");
	lbl_pompletedAcceptanceDate.className = "brlabel-lg";
	lbl_pompletedAcceptanceDate.innerHTML = "取得竣工验收单日期：" + pompletedAcceptanceDate;
	div.appendChild(lbl_services);
	div.appendChild(lbl_projectStart);
	div.appendChild(lbl_pompletedDate);
	div.appendChild(lbl_pompletedAcceptanceDate);
	document.getElementById("serviceList").appendChild(div);
}

//填充日志
function fullLog(ProjectLogJson) {
	var log = document.getElementById("log");
	if (ProjectLogJson == "[]") {
	    log.innerHTML = "尚无日志";
	    $("#log").next().remove();
	} else {
	    var logList = document.createElement("section");
	    logList.id = "cd-timeline";
	    logList.className = "cd-container";
	    insertAfter(logList, log);
	    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象  LogDatesJson   
	    var ProjectLog = JSON.parse(ProjectLogJson.replace(reg, '"'));
	    if (ProjectLog.length < 5) {
	        $("#more").remove();
	    }
	    for (var i = 0; i < ProjectLog.length; i++) {
	        addLog(ProjectLog[i].LogName, ProjectLog[i].Service, ProjectLog[i].LogDate, ProjectLog[i].Name, ProjectLog[i].ID);
	       
	    } 
	}
}

//添加日志(加了个服务)
function addLog(logName, service, date, name, ID) {
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
    h3.innerHTML = "施工说明：" + logName;
    var p_service = document.createElement("p");
    p_service.innerHTML = "服务款项：" + service;
    var p_log = document.createElement("p");
    p_log.innerHTML = "做了1次修改";
    var span = document.createElement("span");
    span.className = "cd-date";
    span.innerHTML = date + " by " + name;
    var a = document.createElement("a");
    a.className = "cd-read-more";
    a.href = "projectLog?ID="+ID;
    a.innerHTML = "详情"
    div_content.appendChild(h3);
    div_content.appendChild(p_service)
    div_content.appendChild(a);
    div_content.appendChild(span);
    div_block.appendChild(div_content);
    logList.appendChild(div_block);
}

function lazyLoad() {
    N = N + 5;
    $.ajax({
        type: "POST",
        url: "/Project/ProjectLogAjaxTT?ID=" + N,
        async: true,
        cache: false,
        success: function (data) {
            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象  LogDatesJson   
            var ProjectLog = JSON.parse(data.replace(reg, '"'));
            if (ProjectLog.length < 5) {
                $("#more").remove();
            } 
            
            if (data =="[]") {
                N -= 5;
                show("暂无最新数据！");
                return false;
            } 
            for (var i = 0; i < ProjectLog.length; i++) {
                if (!document.getElementById("cd-timeline")) {
                    return false;
                }
                addLog(ProjectLog[i].LogName, ProjectLog[i].Service, ProjectLog[i].LogDate, ProjectLog[i].Name, ProjectLog[i].ID);
            }
        },
        error: function () {
            show("请求失败");
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