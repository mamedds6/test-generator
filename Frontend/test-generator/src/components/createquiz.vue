
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
        <b-row class="mc-2" v-for="type in types" :key="type">
        <b-col sm="3">
            </b-col>
            <b-col sm="3">
                <label> {{type.type}} questions</label>
            </b-col>
                <b-col sm="5">
                <b-form-input :id="type-number" :type="number"></b-form-input>
            </b-col>
            <b-col sm="3">
            </b-col>
        </b-row>
    </b-container>

    <br>
    <b-button-group class="mc-3">
    <b-button variant="success" v-on:click="generate">Generate Test</b-button>

    </b-button-group>
    <b-button-group class="mc-3">
    <b-button variant="danger" v-on:click="reset">Reset</b-button>

    </b-button-group>
    
    <br>
    <label> -- Test --  {{questions}} </label>
    


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
                types: [
                    {type:'Easy'},
                    {type:'Medium'}, 
                    {type:'Hard'}
                ],
                message: '',
                questions:"ops",
                                
            }
        },
        methods:{
            reset: function ()
            {
                
            },

            generate: function ()
            {            
                axios
                .get('https://swapi.co/api/people/1')
                .then(
                    response =>
                    {
                        alert(response.data.name); //test generated
                        this.questions = response.data.name;
                    }
                )
                    .catch(function (error) {
                    console.log(error);
                });
            },            
        }
    }
</script>