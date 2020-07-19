





var currentList = {};

//create shopping list
function createShoppingList() {
    currentList.name = $("#shoppingListName").val();
    currentList.items = new Array();
    //Web service call
    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ShoppingListsEF/",
        data: currentList,
        success: function (result) {
            currentList = result;
            showShoppingList();
            //creates object history
            history.pushState({ id: result.id }, result.name, "?id=" + result.id);
        }
    });
}


//display shoping list
function showShoppingList() {

    $("#shoppingListTitle").html(currentList.name);
    $("#shoppingListItems").empty();

    $("#createListDiv").hide();
    $("#shoppingListDiv").show();

    $("#newItemName").val("");
    //puts cursor in new item text input field
    $("#newItemName").focus();
    $("#newItemName").unbind("keyup");
    $("#newItemName").keyup(function (event) {
        //event key code == 13 is "enter" button
        if (event.keyCode == 13) {
            addItem();
        }
    })
}

//add new items
function addItem() {
    var newItem = {};
    newItem.name = $("#newItemName").val();
    newItem.shoppingListId = currentList.id;

    $.ajax({
        type: "POST",
        dataType: "json",
        url: "api/ItemsEF/",
        data: newItem,
        success: function (result) {
            currentList = result;
            //call this function to see the items displayed
            drawItems();
            //empties the text input field after user enters their items
            $("#newItemName").val("");
        }
    });
}

//this displays the items for the currentlist
function drawItems() {
    var $list = $("#shoppingListItems").empty();

    for (var i = 0; i < currentList.items.length; i++) {
        var currentItem = currentList.items[i];
        var $li = $("<li>").html(currentItem.name).attr("id", "item_" + i);
        var $deleteBtn = $("<button onclick='deleteItem(" + currentItem.id + ")'>D</button>").appendTo($li);
        var $checkBtn = $("<button onclick='checkItem(" + currentItem.id + ")'>C</button>").appendTo($li);

        if (currentItem.checked) {
            $li.addClass("checked");
        }
        $li.appendTo($list);
    }
}

function deleteItem(itemId) {
    $.ajax({
        type: "DELETE",
        dataType: "json",
        url: "api/ItemsEF/" + itemId,
        success: function (result) {
            currentList = result;
            drawItems();
        }
    });
}

function checkItem(itemId) {
    var changedItem = {};

    for (var i = 0; i < currentList.items.length; i++) {
        if (currentList.items[i].id == itemId) {
            changedItem = currentList.items[i];
        }
    }

    changedItem.checked = !changedItem.checked;

    $.ajax({
        type: "PUT",
        dataType: "json",
        url: "api/ItemsEF/" + itemId,
        data: changedItem,
        success: function (result) {
            changedItem = result;
            drawItems();
        }
    });
}


function getShoppingListById(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "api/ShoppingListsEF/" + id,
        success: function (result) {
            currentList = result;
            showShoppingList();
            drawItems();
        }
    });
}

function hideShoppingList() {
    $("#createListDiv").show();
    $("#shoppingListDiv").hide();

    $("#shoppingListName").val("");
    //to put cursor into the text field
    $("#shoppingListName").focus();
    //listen for event
    $("#shoppingListName").unbind("keyup");
    $("#shoppingListName").keyup(function (event) {
        //key code = 13 is the keycode for the "enter" button
        if (event.keyCode == 13) {
            createShoppingList();
        }
    });
}


$(document).ready(function () {
    console.info("ready");

    hideShoppingList();

    var pageUrl = window.location.href;
    var idIndex = pageUrl.indexOf("?id=");
    if (idIndex != -1) {
        //+4 is because '?id=' is 4 characters: ? = 1, i = 2, d= 3, '=' equals 4
        getShoppingListById(pageUrl.substring(idIndex + 4));
    }

    window.onpopstate = function (event) {
        if (event.state == null) {
            //hide shopping list
            hideShoppingList();
        }
        else {
            //show shopping list with id from state object
            getShoppingListById(event.state.id);
        }
    };
});

//code for putting strike through prior to ajax
////strike through whenever the "C" button is clicked on an item
//function checkItem(index) {
//    //if already line through, remove (allows user to change mind)
//    if ($("#item_" + index).hasClass("checked")) {
//        $("#item_" + index).removeClass("checked");

//    }
//    //otherwise add the strike through 
//    else {
//        //access the element by index, and add a css styling to class = checked for strike through/line through
//        $("#item_" + index).addClass("checked");
//    }
//}

//delete items prior to implementation with API
//function deleteItem(index) {
//    //deletes the item with the current index, 1 = just to delete that one item
//    currentList.items.splice(index, 1);
//    //call this function to display the appropriate items, with removed item deleted
//    drawItems();
//}
