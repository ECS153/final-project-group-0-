<template>
  <div class="card shadow-m">
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
          url: "http://192.168.1.5:5000/pi",
          method: "post",
          headers: this.$apiHeaders,
          data: {
            SwapId: this.swapProps.id,
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
          this.$toast.error(err.response.data.title);
        }
      });
    }
  },
  //right after creation, get possible credentials that can be used
  created: function() {
    this.$http
      .request({
        url: "http://192.168.1.5:5000/credential",
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
