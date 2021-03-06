import os
import sys
import pandas as pd

data_path = os.getcwd()
df = pd.read_excel(
    os.path.join(data_path, "Q&As.xlsx", ),
    sheet_name="Check lists")

df = df[['PS', 'Checkpoint', 'Question', 'Support Notes', 'Project areas']]
df = df.rename(columns={'PS': 'Barrier', 'Question': 'Text', 'Support Notes': 'SupportNotes', 'Project areas': 'Organization'})

with open(os.path.join(data_path, 'InitQuestions.json'), "w", encoding="utf-8") as file:
    df.to_json(file, orient="records", indent=4, force_ascii=False)
