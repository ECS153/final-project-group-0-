<template>
  <div id="credentials" >
    <credential-list></credential-list>
    <credential-add></credential-add>
    
   
  </div>
</template>
<script>
import CredentialList from "@/components/Credential/List.vue";
import CredentialAdd from "@/components/Credential/Add.vue";

export default {
  name: "CredentialItem",
  components: {
    CredentialList,
    CredentialAdd
  }
};
</script>

<style>
@import "../assets/css/Credential.css";
@import "../assets/css/reset.css";

#credentials {

  display: flex;
  flex-flow: column;
  justify-content: center;
  align-items: center;
}




</style>
