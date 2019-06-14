
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
        <b-row class="mc-2">
        <b-col sm="3">
            </b-col>
            <b-col sm="3">
                <label> Easy questions</label>
            </b-col>
                <b-col sm="5">
                <b-form-input :id="type-number" :type="number" v-model="message1"></b-form-input>
            </b-col>
            <b-col sm="3">
            </b-col>
        </b-row>
    </b-container>
        <b-container fluid>
        <b-row class="mc-2">
        <b-col sm="3">
            </b-col>
            <b-col sm="3">
                <label> Medium questions</label>
            </b-col>
                <b-col sm="5">
                <b-form-input :id="type-number" :type="number" v-model="message2"></b-form-input>
            </b-col>
            <b-col sm="3">
            </b-col>
        </b-row>
    </b-container>
        <b-container fluid>
        <b-row class="mc-2">
        <b-col sm="3">
            </b-col>
            <b-col sm="3">
                <label> Hard questions</label>
            </b-col>
                <b-col sm="5">
                <b-form-input :id="type-number" :type="number" v-model="message3"></b-form-input>
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
    <br>
    <label> -- Test -- </label>
    
        <b-container fluid>
        <b-row class="mc-4"><!-- v-for="type in types" :key="type">
    -->     <b-col sm="3">
            </b-col>
            <b-col>
                <b-table striped hover :items="items"></b-table>
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
                types: [
                    {type:'Easy'},
                    {type:'Medium'}, 
                    {type:'Hard'}
                ],
                testName: 'Test',
                message1: 0,
                message2: 0,
                message3: 0,
                questions:"ops",
                items: []
                                
            }
        },
        methods:{
            reset: function ()
            {
                this.message1 = this.message2 = this.message3= 0;
            },

            generate: function ()
            {            
                axios
                .get('http://10.160.47.210:5001/api/quiz?easyNumber='+this.message1+'&mediumNumber='+this.message2+'&hardNumber='+this.message3)
                .then(
                    response =>
                    {
                        var max = 0;
                        response.data.forEach(Q => {
                                if(max <  Q.answers.length)
                                    max = Q.answers.length;
                            }
                        );
                        var i = 0;
                        var j = 1;
                        var correctOne = "";
                        var questions = [];
                        response.data.forEach(Q => {
                            questions.push({})
                            questions[i].Question = Q.content;
                            for(var k = 0; k < max; k++)
                                questions[i]["Answer" + (k+1)] = "";
                            Q.answers.forEach(element => {
                                if(element.isCorrect)
                                    correctOne = j;
                                questions[i]["Answer" + j] = element.content; 
                                j++;
                            });
                            // questions[i].Correct = correctOne;
                            questions[i].Difficulty = Q.difficulty;
                            questions[i]._cellVariants = {};
                            questions[i]._cellVariants["Answer" + correctOne] = 'info';
                            correctOne = "";
                            i++;
                            j = 1;
                        });
                        this.items = questions;
                    }
                ) 
            }          
        }
    }
</script>