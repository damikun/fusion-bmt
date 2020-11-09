
import * as React from 'react';
import PreparationView from '../views/PreparationView';
import { Participant, Evaluation, Progression, Organization, Role, Project, Question, Barrier, Status, Answer, Severity } from '../api/models';

const PreparationRoute = () => {

    let project: Project = {
        createDate: new Date(),
        evaluations: [],
        fusionProjectId: "fusion-project-id",
        id: "project-id"
      }

    let evaluation: Evaluation = {
        createDate: new Date(),
        id: "evaluation-id",
        name: "Evaluation name",
        participants: [],
        progression: Progression.ALIGNMENT,
        project: project,
        projectId: "project-id",
        questions: []
    };

    let participant: Participant = {
        createDate: new Date(),
        evaluation: evaluation,
        evaluationId: evaluation.id,
        fusionPersonId: "fusion-id",
        id: "participant-id",
        organization: Organization.PREOPS,
        role: Role.READONLY
    };

    let dummyQuestion: Question = {
        actions: [],
        answers: [],
        barrier: Barrier.GM,
        createDate: new Date(),
        evaluation: evaluation,
        evaluationId: evaluation.id,
        id: "question-id",
        organization: Organization.ENGINEERING,
        status: Status.ACTIVE,
        supportNotes: "There are the support notes",
        text: "This is the question text"
    };

    let dummyQuestion2: Question = {
        actions: [],
        answers: [],
        barrier: Barrier.GM,
        createDate: new Date(),
        evaluation: evaluation,
        evaluationId: evaluation.id,
        id: "question-id",
        organization: Organization.ENGINEERING,
        status: Status.ACTIVE,
        supportNotes: "There are the support notes",
        text: "This is the question text"
    };

    let dummyQuestion3: Question = {
        actions: [],
        answers: [],
        barrier: Barrier.PS1,
        createDate: new Date(),
        evaluation: evaluation,
        evaluationId: evaluation.id,
        id: "question-id",
        organization: Organization.ENGINEERING,
        status: Status.ACTIVE,
        supportNotes: "There are the support notes",
        text: "This is the question text"
    };

    let dummyAnswer: Answer = {
        answeredBy: participant,
        createDate: new Date(),
        id: "answer-id",
        progression: Progression.ALIGNMENT,
        question: dummyQuestion,
        questionId: dummyQuestion.id,
        severity: Severity.HIGH,
        text: "Answer text",
    };

    project.evaluations = [evaluation];
    evaluation.participants = [participant];
    evaluation.questions = [dummyQuestion, dummyQuestion2, dummyQuestion3];
    dummyQuestion.answers = [dummyAnswer];
    dummyQuestion2.answers = [dummyAnswer];
    dummyQuestion3.answers = [dummyAnswer];

    return (
        <PreparationView evaluation={evaluation}/>
    );
};

export default PreparationRoute;
