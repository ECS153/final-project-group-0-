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
          url: "http://192.168.1.5:5000/swap",
          method: "post",
          headers: this.$apiHeaders,
          data: {
            SwapId: this.swapProps.id,
            PrivateKey: '<?xml version="1.0" encoding="utf-16"?><RSAParameters xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><D>5C5wY1sOt8lgZSfqF1ACi5jgHFK++SmoqxA7yPcz02ILo2KxbQSyx8U7ojzpOLObCCWP722IDTq72TXF4uNpXlWNP6/Fsb93dp7VWaQtmZkjvvMV0bP4NTrQzNInh8qInvTdFj4EUSQm6ktJjh7vek02jS5b9B5ejtIvdtJJF3V+itlq9aPFMBy3qSuJzESh9+JGXQWtndnEQY2mEFMvdjAQfUf5xoDImep1Jf7wN/liiP7skXggJ9X3CAcyNuSHq2gxnzFVCYteMmpugSNlx/huHAq+69sHmO8lWiYO1A4If2t3nD+iHTzf8IYOgnxng8Wl3USfQIr+Wxi2NRREjQ==</D>  <DP>HkaMP13iq3rxKJ1AzOnrqKnk7m4/8zOgAR/YS4xQlvBgx7TzO6Mpj5KFLwnCO0CDbMCKzzM0+vkW3Z6pTQ5C71oBCkWQyKnspsddKjGZYcJqN7KWFijpSPTDV7uQVWGM1gioGYLVVP/Ywpy8vwM3xEp/HzvmKKV7w5CfjmNEEEc=</DP><DQ>Gn3O/1YARykhAk4noTvzf8iB6pZ8fwwDstdCgsHvY6JFc2pZB2HafqDKlKvFEPz32bUgm2J6qN1FO31r+QJ/06XjS8m629gJpAlkZJi1b2LLiuvH0oCuYHSuNNibarEchaXTxWIfVAti2jxSMWw/dftXGiU6TubYvvSN3dkcui8=</DQ><Exponent>AQAB</Exponent><InverseQ>o2XfEwX4USrpKOt3LQZh/mrXmGaBoGR4Tx7a9wGlsxjGW1xnLPJNKzMXQmUwAd4z7RwT6Nt9SaWvU+sgzhUbYXr68Sxs3Av0f0gqjyhdXm2uldP7UERL3bX//5chNPL99zL2XbR8RTeQ8Mdtm00UykgkIIncxj9SmNe/Ry8hm3c=</InverseQ><Modulus>67pFZDVKmvd9eVdDC2qeNZAtu9JhnYPnTnJ+uT5k4v07WI6+Eq0bzTPppFOcWVSRd1MHMvXnynQqpUVsyMHHlZ+LesWyzziXyW9U7zT4IlgQuRLYITGx/CV02c8d7h2QluFoc7b4V2yYnIyPOqf3e3cK+1nT86lAEwzKDA6CxwCNLASDtU/5VyC5wzaSMoBufmVyFbvBmunNB/nfQnSAj6PTfSn/E3TB4ceGUB7cuT/XV+8xTnUPKPrel9zDsb/uNSH68eEnnwcBNERgnVBG/8N3EazncSFF55sXJvO1FMk2fChQAdllsOI/gMpVhLicJRHMgyYdEf9pwrS/d5HAmQ==</Modulus><P>+bz+/5XdDSqFUYBiR/ERUmBDzfc/fa62NGMovZxwn1WagUOZS/V4Tn6Gq/bj1rdMPqM4WN4TmLM1/nApGwUO+HVMiPYDW8HrViDtt1U2VkheaaXm06nB32SzruU53jpkdAurIkgTEdtCfN/avm2UCsU9wo3iTzBgsscJR7gpGZc=</P><Q>8aNYJzp0aIRDiK6DKsbE4XcrwbvFny0rZFT9VBEJSLVSr4FQ+hTNrBofwzpjSqVH46xsea3Eng0dfjA5PJ+CO2uZlqTGcVWr2Fhgjmp2gg6R7fJ8tt63rSmswIBGzpmtyyNJ1RkStio3tiVIthV7Ca8otN5YlIvMeOcNhJ0BXU8=</Q></RSAParameters>',
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
