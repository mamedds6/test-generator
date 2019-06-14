
<template>
<b-container fluid>
    <b-row class="my-1" v-for="type in types" :key="type">
        <b-col sm="3">
            <label>Type Number:</label>
        </b-col>
        <b-col sm="9">
            <b-form-input :id="type-number" :type="number"></b-form-input>
        </b-col>
    </b-row>
</b-container>
</template>

<script>
    export default {
        name: 'createquiz',
        components: {
            
        },
        data() {
            return {
            }
        }
    }
</script>