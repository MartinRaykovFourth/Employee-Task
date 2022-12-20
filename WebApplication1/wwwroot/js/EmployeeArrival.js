"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/employeeArrivalHub").build();

$(function () {
	connection.start().catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("RecievedArrivals", function (arrivals) {
	BindArrivalsToTable(arrivals);
});

function BindArrivalsToTable(arrivals) {
	var tr;
	$.each(arrivals, function (index, employee) {
		tr = $('<tr/>');
		tr.append(`<td>${employee.employeeId}</td>`);
		tr.append(`<td>${employee.forename}</td>`);
		tr.append(`<td>${employee.surname}</td>`);
		tr.append(`<td>${employee.role}</td>`);
		tr.append(`<td>${employee.teams}</td>`);
		tr.append(`<td>${employee.arrivalTime}</td>`);
		tr.append(`<td>${employee.managerId}</td>`);
		$('#tblArrivals').append(tr);
	});
}