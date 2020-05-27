<template>
    <div id="credential-list">
        <h2 class="title"> Passwords</h2>
        <div class="credentials card">
            <div class="header">
                <h4> Domain </h4>
                <h4>Hint</h4>
            </div>
            <credential-item v-for="(cred, index) in pwCred" :key="index" :cred="cred"></credential-item>
        </div>
        <h2 class="title"> Credit Cards </h2>
        <div class="credentials card">
            <div class="header">
                <h4> Domain </h4>
                <h4>Hint</h4>
            </div>
            <credential-item v-for="(cred, index) in ccCred" :key="index" :cred="cred"></credential-item>
        </div>
        <h2 class="title"> Usernames </h2>
        <div class="credentials card">
            <div class="header">
                <h4> Domain </h4>
                <h4>Hint</h4>
            </div>
            <credential-item v-for="(cred, index) in userCred" :key="index" :cred="cred"></credential-item>
        </div>
        <h2 class="title"> Emails </h2>
        <div class="credentials card">
            <div class="header">
                <h4> Domain </h4>
                <h4>Hint</h4>
            </div>
            <credential-item v-for="(cred, index) in emailCred" :key="index" :cred="cred"></credential-item>
        </div>
    </div>

</template>

<script>

import CredentialItem from "@/components/Credential/Item.vue";
export default {
    name: "CredentialList",
    components:{
        CredentialItem
    },
    props: ["token"],
    data() {
        return {
            creds: [{ hint: "", id: "", type: "", domain:"" }]
        };
    },
    computed: {
        pwCred: function() {
            return this.creds.filter(function(cred){
                return cred.type == 0;
            });
        },
        ccCred: function() {
            return this.creds.filter(function(cred){
                return cred.type == 1;
            });
        },
        userCred: function() {
            return this.creds.filter(function(cred){
                return cred.type == 2;
            });
        },
        emailCred: function() {
            return this.creds.filter(function(cred){
                return cred.type == 3;
            });
        },
    },
    created: function() {
    this.$http
      .request({
        url: "http://192.168.1.5:5000/credential",
        method: "get",
        headers: {
          Authorization: "BEARER " + this.$loginToken,
          "Content-Type": "application/json"
        },

      })
      .then(resp => {
        if (resp.status == 200) {
          this.creds = resp.data;
        }
      });
  }
};
</script>

<style scoped>

#credential-list {
    width: 510px;
    display: flex;
    flex-flow:column;
    justify-content: stretch;
    align-items: stretch;
}
.credentials {

    width: 100%;
    padding-bottom: 30px;
    margin-bottom: 100px;
}
.header {
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    height: 40px;
    background-color: #ff5983;
    color: white;
    display: flex;
    justify-content: flex-start;
    align-items: center;
}
.header h4:first-child {
    padding-left: 70px;
    width: 230px;
}

</style>