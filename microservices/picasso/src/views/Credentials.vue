<template>
  <div id="credentials">
    <credential-list></credential-list>
    <credential-add></credential-add>
  </div>
</template>
<script>
import "@/assets/css/views/credential.css";
import CredentialList from "@/components/Credential/List.vue";
import CredentialAdd from "@/components/Credential/Add.vue";

export default {
  name: "Credentials",
  components: {
    CredentialList,
    CredentialAdd
  }
};
</script>
