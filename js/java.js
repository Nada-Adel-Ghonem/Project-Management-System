function openNav() {
    document.getElementById("mySidenav").style.width = "300px";
    document.getElementById("main").style.marginLeft = "300px";
    document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
}
		  
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft= "0";  
    document.body.style.backgroundColor = "white";
}

function openBoards() {
    if ($("#plusBoards").attr("class") == "fas fa-plus") {
      $("#plusBoards").removeClass("fas fa-plus");
      $("#plusBoards").addClass("fas fa-minus");
      $(".SideMenuBoards").css("display", "block");
    } else {
      $("#plusBoards").removeClass("fas fa-minus");
      $("#plusBoards").addClass("fas fa-plus");
      $(".SideMenuBoards").css("display", "none");
    }
}
		 

	
function openCloseBackgrounds(){
     if($("#plusBackgrounds").attr("class")=="fas fa-minus"){
	    $(".backgrounds").css("display","none");
	    $("#plusBackgrounds").removeClass("fas fa-minus");
	    $("#plusBackgrounds").addClass("fas fa-plus");
     }else{
	    $(".backgrounds").css("display","block");
	    $("#plusBackgrounds").removeClass("fas fa-plus");
	    $("#plusBackgrounds").addClass("fas fa-minus");
     }
}

	
var background = ["url('../../images/2.jpg')", "url('../../images/3.jpg')", "url('../../images/4.jpg')", "url('../../images/6.jpg')", "url('../../images/7.jpg')", "url('../../images/8.jpg')", "url('../../images/9.jpg')", "url('../../images/13.jpg')"];


function changeBackground(idx) {
	  document.body.style.backgroundImage = background[idx];

      var BoardDto = {
		  "Name": $("#Board").attr("data-board-name"),
		  "UserId": $("#Board").attr("data-board-userid"),
		  "NumOfLists": parseInt($(".board").attr("data-board-numof-lists")),
          "Background":background[idx]
      };

      $.ajax({
		  url: "../../api/boards/" + $("#Board").attr("data-board-id"),
          data: JSON.stringify(BoardDto),
          type: "PUT",
          contentType: "application/json; charset=utf-8"
      });
  }

function closeList(item) {
	$.ajax({
        url: "../../api/lists/" + $(item).parents(".card").attr("data-list-id"),
        method: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
	$(item).parents(".card").css("display", "none");

	var board = {};
	board.UserId = $(item).parents(".board").attr("data-board-userId");
	board.NumOfLists = parseInt($(item).parents(".board").attr("data-board-numOf-lists")) - 1;
    board.Name = $(item).parents(".board").attr("data-board-name");
    board.Background = $(item).parents("#Board").css('background-image');

    $.ajax({
		url: "../../api/boards/" + $(item).parents(".board").attr("data-board-id"),
        data: JSON.stringify(board),
        type: "PUT",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $(item).parents(".board").attr("data-board-numOf-lists",
                parseInt($(item).parents(".board").attr("data-board-numOf-lists")) - 1);
        }
    });

}

function AddNewList(item){
   var button = $(item);
   var list =`<div class="card" style="display:inline-block;background-color:#E6E6E6;border-color:transparent;">
	  
				  <input onfocus="lightListHeader(this)" onfocusout="darkListHeader(this)" onchange="updateListHeader(this)" class="card-header" placeholder="Card Title" style="margin: 3px 3px 3px 15px;font-weight: bold;width: 77%;border:none;background-color: transparent;"/> 
                 
                    <button onclick="closeList(this)" class="btn" style="font-size: 36px;margin-left:5px;">&times;</button>

				  <div data-numof-events="0" class="card-body" style="max-height:530px;overflow-y:auto;padding-right:8px;">
						  <div class="creationCard" onclick="AddNewCard(this)" style="margin-top: 10px;">
							  <i class='fas fa-plus' style='font-size:16px;margin-right: 5px;'></i>
								  <strong>Add another card</strong>
						  </div>
				  </div>
				  </div>`;

   var listvar = {};

   listvar.NumOfEvents = 0;
 
   listvar.BoardId = button.parents(".board").attr("data-board-id");
  $.ajax({
      url: "../../api/lists",
      data: JSON.stringify(listvar),
      method: "POST",
      contentType: "application/json; charset=utf-8",
      dataType: "json",
	  success: function (result) {
		  $(item).parents("#creationList").prev().attr("data-list-id", result.Id);
      }
  });
   $("#creationList").before(list);
   $("#scrollbar")[0].scrollLeft += 500;

   var board = {};
   board.UserId = $(item).parents(".board").attr("data-board-userId");
   board.NumOfLists = parseInt($(item).parents(".board").attr("data-board-numOf-lists")) + 1;
   board.Name = $(item).parents(".board").attr("data-board-name");
   board.Background = $(item).parents("#Board").css('background-image');

   $.ajax({
       url: "../../api/boards/" + $(item).parents(".board").attr("data-board-id"),
       data: JSON.stringify(board),
       type: "PUT",
       contentType: "application/json; charset=utf-8",
       success: function () {
           $(item).parents(".board").attr("data-board-numOf-lists",
               parseInt($(item).parents(".board").attr("data-board-numOf-lists")) + 1);
       }
   });
}


function updateListHeader(item) {

	var listDto = {
			"Name": $(item).val(),
		"NumOfEvents": $(item).siblings(".card-body").attr("data-numof-events"),
			"BoardId": $(item).parents(".board").attr("data-board-id")
    };

    $.ajax({
		url: "../../api/lists/" + $(item).parents(".card").attr("data-list-id"),
		data: JSON.stringify(listDto),
        type: "PUT",
		contentType: "application/json; charset=utf-8"
    });
}

function AddNewCard(item){
  var textarea =`<div class="sign textarea-container" style="width:280px;display:inline-block;">
    <textarea onfocusout="updateEvent(this)" onmouseover="CardHoverOn(this)" onmouseleave="CardHoverOff(this)" class="newCard" name="Description" style="margin: 3px; border-radius: 4px;" cols="25"></textarea>

    <button type="button" onclick="displayFinalCard(this)" class="AddCard btn btn-dark" >Add Card </button>
    <button onclick="closeCard(this)" class="btn btn-dark" style="margin-left:5px;" >&times;</button>
	  </div>`;

      

	  $(item).css("display","none");
	  $(item).before(textarea);
	
	  
	  $(".newCard").css("background-color","white");	
}


function updateEvent(item) {
  if ($(item).parents(".sign").attr("data-event-id") != null) {
    var eventDto = {
        "Description": $(item).val(),
        "ListId": $(item).parents(".card").attr("data-list-id")
    };

    $.ajax({
        url: "../../api/events/" + $(item).parents(".sign").attr("data-event-id"),
        data: JSON.stringify(eventDto),
        type: "PUT",
        contentType: "application/json; charset=utf-8"
    });
  }
}


function displayFinalCard(item) {

      var button = $(item);
	  var value = button.prev().children(".newCard").val();


	  if (value !== "") {

          var eventt = {};

      
          eventt.Description = button.siblings(".newCard").val();
          eventt.ListId = button.parents(".card").attr("data-list-id");
          $.ajax({
              url: "../../api/events",
              data: JSON.stringify(eventt),
              method: "POST",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
			  success: function (result) {
				  $(item).parents(".sign").attr("data-event-id", result.Id);
                  $(item).parents(".sign").attr("DescriptionIsFilled", true);
			  }
		  });
          $(item).before(`<button onclick="closeCard(this)" onmousemove="CloseCardHoverOn(this)" onmouseleave="CloseCardHoverOff(this)" class="btn btnhover">&times;</button>`);
          $(item).css("display", "none");
          $(item).next().css("display", "none");
          $(item).parents("div").siblings(".creationCard").css("display", "inline-block");

		  var list = {};
          list.Name = $(item).parents(".card-body").siblings(".card-header").val();
		  list.NumOfEvents = parseInt($(item).parents(".card-body").attr("data-numof-events")) + 1;
          list.BoardId = $(item).parents(".board").attr("data-board-id");
          $.ajax({
              url: "../../api/lists/" + $(item).parents(".card").attr("data-list-id"),
              data: JSON.stringify(list),
              type: "PUT",
              contentType: "application/json; charset=utf-8",
              success: function () {
                  $(item).parents(".card-body").attr("data-numof-events",
                      parseInt($(item).parents(".card-body").attr("data-numof-events")) + 1);
              }
          });
      }

}


function closeCard(item) {
      if (($(item).prev().attr("class") === "AddCard btn btn-dark" && $(item).prev().css("display") === "none") || $(item).prev().attr("class")==="newCard") {
		  var button = $(item);

          $.ajax({	
              url: "../../api/events/" + $(item).parents(".sign").attr("data-event-id"),
              method: "DELETE",
              contentType: "application/json; charset=utf-8",
              dataType: "json"
		  });

		  var list = {};
          list.Name = $(item).parents(".card-body").siblings(".card-header").val();
          list.NumOfEvents = parseInt($(item).parents(".card-body").attr("data-numof-events")) - 1;
          list.BoardId = $(item).parents(".board").attr("data-board-id");
          $.ajax({
              url: "../../api/lists/" + $(item).parents(".card").attr("data-list-id"),
              data: JSON.stringify(list),
              type: "PUT",
              contentType: "application/json; charset=utf-8",
              success: function () {
                  $(item).parents(".card-body").attr("data-numof-events",
                  parseInt($(item).parents(".card-body").attr("data-numof-events")) - 1);
              }
          });
	  }
      $(item).css("display", "none");
	$(item).siblings(".newCard").css("display", "none");
    $(item).siblings(".AddCard").css("display", "none");
    $(item).parents(".sign").next().css("display", "block");
	$(item).parents(".sign").css("display", "none");

}
      
		   
function lightListHeader(item){
  $(item).css("background-color","white");
}

function darkListHeader(item){
  $(item).css("background-color","transparent");
}



function hideEmailValidation(item) {
    $(item).siblings("#EmailValidation").css("display", "none");
}

function validateEmail() {
	if (document.getElementById("Emailup").value.indexOf(".") == -1 || document.getElementById("Emailup").value.indexOf("@") == -1) {
		checkedd.classList.remove("validd");
		checkedd.classList.add("invalidd");
	} else {
		checkedd.classList.remove("invalidd");
		checkedd.classList.add("validd");
	}
}

function validateConfirmPassword() {
	if (document.getElementById("PasswordUp").value === document.getElementById("confirmPassword").value) {
		accuratee.classList.remove("invalidd");
		accuratee.classList.add("validd");
	} else {
		accuratee.classList.remove("validd");
		accuratee.classList.add("invalidd");
	}
}

function validatePassword() {
	if (document.getElementById("PasswordUp").value.match(/[A-Z]/g)) {
        CapitalLetter.classList.remove("invalidd");
        CapitalLetter.classList.add("validd");
	} else {
        CapitalLetter.classList.remove("validd");
        CapitalLetter.classList.add("invalidd");
	}
	if (document.getElementById("PasswordUp").value.match(/[a-z]/g)) {
        SmallLetter.classList.remove("invalidd");
        SmallLetter.classList.add("validd");
	} else {
        SmallLetter.classList.remove("validd");
        SmallLetter.classList.add("invalidd");
	}

	if (document.getElementById("PasswordUp").value.match(/[0-9]/g)) {
        NumberChar.classList.remove("invalidd");
        NumberChar.classList.add("validd");
        console.log("Number");
    } else {
        NumberChar.classList.remove("validd");
        NumberChar.classList.add("invalidd");
    }

	if (document.getElementById("PasswordUp").value.length >= 8) {
        PasswordLength.classList.remove("invalidd");
        PasswordLength.classList.add("validd");
        console.log("Length");
    } else {
        PasswordLength.classList.remove("validd");
        PasswordLength.classList.add("invalidd");
	}
}

function displayEmailValidation(item) {
    validateEmail();
    $(item).siblings("#EmailValidation").css("display", "inline-block");
}

function displayPasswordValidation(item) {
    validatePassword();
    $(item).siblings("#PasswordValidation").css("display", "block");
}
function displayConfirmPasswordValidation(item) {
    validateConfirmPassword();
    $(item).siblings("#ConfirmPasswordValidation").css("display", "inline-block");
}

function hidePasswordValidation(item) {
    $(item).siblings("#PasswordValidation").css("display", "none");
}

function hideConfirmPasswordValidation(item) {
    $(item).siblings("#ConfirmPasswordValidation").css("display", "none");
}


$("#LogInFormView").css("background-image", "url('../images/signup-bg.jpg')");
$("#LogInFormView").css("margin", "10% 30%");

$("#RegisterFormView").css("background-image", "url('../images/signup-bg.jpg')");
$("#RegisterFormView").css("margin", "5% 30%");

$("#UpdateUser").css("background-image", "url('../../images/signup-bg.jpg')");
$("#UpdateUser").css("margin", "5% 30%");


$("#createopen").css("background-image", "url('../../images/signup-bg.jpg')");

$("#OpenCreateBoards").css("background-image", "url('../../images/signup-bg.jpg')");
$("#OpenCreateBoards").css("margin", "10% 30%");

function CardHoverOn(item) {
	$(item).siblings(".btnhover").css("display", "inline-block");
}

function CardHoverOff(item) {
	$(item).siblings(".btnhover").css("display", "none");
}
function CloseCardHoverOn(item) {
    $(item).css("display", "inline-block");
}

function CloseCardHoverOff(item) {
    $(item).css("display", "none");
}

$(".Create").click(function () {
    $(".OpenForm").css("display", "none");
    $(".createForm").css("display", "block");
    $(".Create").css("border-bottom", "3px solid #98ded9")
    $(".Open").css("border-bottom", "none")
});

$(".Open").click(function () {
    $(".createForm").css("display", "none");
    $(".OpenForm").css("display", "block");
    $(".Open").css("border-bottom", "3px solid #98ded9")
    $(".Create").css("border-bottom", "none")
});

$("#OpenCreateBoards").ready(function() {
    $('#myModal').modal('show');
});

