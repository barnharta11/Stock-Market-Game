<template>
<div>
  <button class="open-button" @click="openForm()">Rosebud</button>
     <div class="form-popup" id="myForm">
        <form action="/action_page.php" class="form-container">
        <img src="https://media.metrolatam.com/2019/10/16/18ps27-638137169a2d73f9f63481c115a7f092.jpg" alt="Harold">
          <h2>Looking for some insider trading tips??</h2>
        <button type="submit" class="btn" id="auditButton" @click="pushAuditPic()">YES!</button>
          <button type="button" class="btn cancel" @click="closeForm()">NO!</button>
        </form>
      </div> 
</div>
    
</template>

<script>
export default {
    
    methods:{
      
      openForm() {
      document.getElementById("myForm").style.display = "block";
      },
      closeForm() {
      document.getElementById("myForm").style.display = "none";
      },
      pushAuditPic() {
       this.$router.push('/IRS');
      }, 

    },

}
</script>

<style>
/* Harold Button */

body {font-family: Arial, Helvetica, sans-serif;}
* {box-sizing: border-box;}

.open-button {
  background: none;
  color: rgb(105, 172, 105);
  padding: 16px 20px;
  border: none;
  cursor: pointer;
  opacity: 0.8;
  position: fixed;
  bottom: 23px;
  right: 28px;
  width: 150px;
}

/* The popup form - hidden by default */
.form-popup {
  display: none;
  position: fixed;
  bottom: 0;
  right: 15px;
  border: 3px solid #f1f1f1;
  z-index: 9;
}

/* Add styles to the form container */
.form-container {
  max-width: 300px;
  padding: 10px;
  background-color: white;
}

/* Full-width input fields */
.form-container input[type=text], .form-container input[type=password] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  border: none;
  background: #f1f1f1;
}

/* When the inputs get focus, do something */
.form-container input[type=text]:focus, .form-container input[type=password]:focus {
  background-color: #ddd;
  outline: none;
}

/* Set a style for the submit/login button */
.form-container .btn {
  background-color: #4CAF50;
  color: white;
  padding: 16px 20px;
  border: none;
  cursor: pointer;
  width: 100%;
  margin-bottom:10px;
  opacity: 0.8;
}

/* Add a red background color to the cancel button */
.form-container .cancel {
  background-color: red;
}
.form-container img {
height: 235px;

}

/* Add some hover effects to buttons */
.form-container .btn:hover, .open-button:hover {
  color: white;
}
</style>