
import * as React from 'react';
import { Chip, NavigationStructure, NavigationDrawer } from '@equinor/fusion-components';
import { Barrier, Answer, Evaluation, Question, } from '../api/models';
import QuestionAndAnswerForm from '../components/QuestionAndAnswer/QuestionAndAnswerForm';

interface BarrierQuestionsProps
{
    title: string
    questions: Question[]
}

const BarrierQuestions = ({title, questions}: BarrierQuestionsProps) => {
    return (
        <>
            <h2>{title}</h2>
            {questions.map((question) => {
                return (
                    <>
                        <hr style={{ height: "1", color: "lightgray" }}/>
                        <QuestionAndAnswerForm
                            question={question}
                            answer={question.answers[0]}
                            questionNumber={1}
                            onAnswerChange = {(answer: Answer) => {

                            }}
                        />
                    </>
                )
            })}
            <br/>
        </>
    )
}

interface PreparationViewProps
{
    evaluation: Evaluation
}

const PreparationView = ({evaluation}: PreparationViewProps) => {
    const [selectedBarrier, setSelectedBarrier] = React.useState<string>(Object.values(Barrier)[0]);
    const [structure, setStructure] = React.useState<NavigationStructure[]>(
        Object.entries(Barrier).map(([shortName, barrierName]) => {
            return {
                id: barrierName,
                type: 'grouping',
                title: barrierName,
                icon: <>{shortName}</>,
                aside: <Chip title="3/10" />
            }
        })
    );

    return (
        <div style={{ display: "flex", height: "100%" }}>
            <div style={{ flex:  "0 0 20%" }}>
                <NavigationDrawer
                    id="navigation-drawer-story"
                    structure={structure}
                    selectedId={selectedBarrier}
                    onChangeSelectedId={(selectedItem) => setSelectedBarrier(selectedItem)}
                    onChangeStructure={(newStructure) => {
                        setStructure(newStructure);
                    }}
                />
            </div>
            <div style={{ width: 1, flexGrow: 1, margin: 20 }}>
                    <BarrierQuestions title={selectedBarrier} questions={evaluation.questions.filter(q => q.barrier === selectedBarrier)} />
            </div>
        </div>

    );
};

export default PreparationView;
