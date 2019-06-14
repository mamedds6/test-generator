
<template>
<div>
    <h2>Test name</h2>
    
<b-container fluid>
  <b-row class="mc-1">
    <b-col sm="3">
    </b-col>
    <b-col sm="8">
      <b-form-textarea id="textarea" v-model="message" placeholder="Write the name of the test here..." rows="1" max-rows="6" ></b-form-textarea>
    </b-col>
    <b-col sm="3">
    </b-col>
  </b-row>
</b-container>

<br>
<h2>Number of questions</h2>
<b-container fluid>
    <b-row class="mc-2"><!-- v-for="type in types" :key="type">
-->     <b-col sm="3">
        </b-col>
        <b-col sm="3">
            <label>number of *level* questions</label>
        </b-col>
            <b-col sm="5">
            <b-form-input :id="type-number" :type="number"></b-form-input>
        </b-col>
        <b-col sm="3">
        </b-col>
    </b-row>
</b-container>

</div>
</template>

<script>
    import axios from 'axios';

    export default {
        name: 'createquiz',
        components: {
            
        },
        data() {
            return {
                
            }
        },
        methods:{
                save: function ()
            {            
                axios
                .get('https://swapi.co/api/people/1')
                .then(
                    response =>
                    {
                        alert(response.data.name);
                    }
                );
            },            
        }
    }
</script>