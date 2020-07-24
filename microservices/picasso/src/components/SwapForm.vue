<template>
  <div class="card shadow-m">
    <h2 class="primary-text" id="authId">{{ swapProps.authId }}</h2>
    <h2 class="primary-text"><span class="title">Domain: </span>{{ swapProps.domain }}</h2>
    <h2 class="primary-text"><span class="title">Field-Id: </span>{{ swapProps.fieldId }}</h2>
    <select v-model="selectedCred" id="creds">
      <option v-for="cred in creds" v-bind:value="cred" :key="cred.id">
        {{cred.hint}}
      </option>
    </select>
    <button @click="submitSwap" class="button primary">Submit</button>
  </div>
</template>

<script>
export default {
  name: "SwapForm",
  props: ["swapProps"],
  data() {
    return {
      selectedCred: { hint: "", id: "" },
      creds: ""
    };
  },
  methods: {
    submitSwap: function() {
      this.$http
        .request({
          url: "http://localhost:8000/swap",
          method: "post",
          headers: this.$apiHeaders,
          data: {
            SwapId: this.swapProps.id,
            PrivateKey: this.$privateKey,
            CredentialId: this.selectedCred.id
          }
        })
        .then(resp => {
          if (resp.status == 200) {
            this.$emit("submit");
          }
        })
        .catch ((err) => {
        if (err.response.status == 401) {
          this.$toast.error("Unauthorized");
        } else {
          console.log(this.$PrivateKey);
          this.$toast.error(err.response.data.title);
        }
      });
    }
  },
  //right after creation, get possible credentials that can be used
  created: function() {
    this.$http
      .request({
        url: "http://localhost:8000/credential",
        method: "get",
        headers: this.$apiHeaders,
        params: {
          domain: this.swapProps.domain,
          type: this.swapProps.type
        }
      })
      .then(resp => {
        if (resp.status == 200) {
          this.creds = resp.data;
        }
      })      
      .catch ((err) => {
        if (err.response.status == 401) {
          this.$toast.error("Unauthorized");
        } else {
          this.$toast.error(err.response.data.title);
        }
      });
  }
};
</script>

<style scoped>

#authId {
  font-size: 48px;
  align-self: center;
}

div {
  width: 400px;
  height: 300px;
  padding: 30px;
  display: flex;
  flex-flow: column;
  justify-content: space-around;
  align-items: stretch;
}
h2 {
  font-size: 24px;
  padding: 10px;
}
</style>
