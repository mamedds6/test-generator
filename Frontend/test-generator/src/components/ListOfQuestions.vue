<template>
<div>
    <b-container fluid>
    <b-row class="mc-2"><!-- v-for="type in types" :key="type">
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
        name: 'lisofquestions',
        components: {
            
        },
        data() {
            return {
            //     items: [
            //     { age: 40, first_name: 'Dickerson', last_name: 'Macdonald' },
            //     { age: 21, first_name: 'Larsen', last_name: 'Shaw' },
            //     { age: 89, first_name: 'Geneva', last_name: 'Wilson' },
            //     { age: 38, first_name: 'Jami', last_name: 'Carney' }
            // ]
            items: []
            }
        },
        methods:
        {
            getData: function()
            {
                axios
                .get('http://10.160.47.210:5001/api/quiz/all')
                // .get('http://195.80.130.120:9000/api/quiz/')
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
        },
        beforeMount(){
            this.getData()
        }
    }
</script>