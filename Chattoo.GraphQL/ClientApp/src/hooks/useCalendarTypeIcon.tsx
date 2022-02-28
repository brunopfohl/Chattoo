import { Celebration, QuestionMark, RecordVoiceOver, SportsBar } from "@mui/icons-material";
import { CalendarEventTypeGraphType } from "graphql/graphql-types";

export const useCalendarEventTypeIconRenderer = () => {
    const renderIcon = (type: CalendarEventTypeGraphType | undefined | null) => {
        switch (type) {
            case CalendarEventTypeGraphType.Drink: {
                return <SportsBar />
            }
            case CalendarEventTypeGraphType.Celebration: {
                return <Celebration />
            }
            case CalendarEventTypeGraphType.Brainstorming: {
                return <RecordVoiceOver />
            }
            default: {
                <QuestionMark />
            }
        }
    };

    return renderIcon;
}