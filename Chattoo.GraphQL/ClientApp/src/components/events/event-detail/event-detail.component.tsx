import { AppStateContext } from "@components/app-state-provider.component";
import { AppUser } from "common/interfaces/app-user.interface";
import { CalendarEvent, CommunicationChannel, useAddUserToCalendarEventMutation, useDeleteCalendarEventMutation, User, useRemoveUserFromCalendarEventMutation } from "graphql/graphql-types";
import { useRouter } from "next/router";
import { FC, useContext, useMemo, useCallback } from "react";
import EventDetailHeader from "./event-detail-header.component";
import EventDetailInfo from "./event-detail-info.component";
import EventDetailParticipants from "./event-detail-participants.component";

export interface EventDetailProps {
    calendarEvent: CalendarEvent;
    channel?: CommunicationChannel | null;
    participants: User[];
    canEdit: boolean;
    refetchParticipants: () => void;
}

const EventDetail: FC<EventDetailProps> = (props) => {
    const { calendarEvent, channel, participants, refetchParticipants, canEdit } = props;
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const router = useRouter();

    const isJoined = useMemo(() => {
        return participants.some(p => p.id === user?.id)
    }, [participants]);

    const [addUserToCalendarEvent] = useAddUserToCalendarEventMutation();

    const onUserAdd = useCallback((user: AppUser) => {
        addUserToCalendarEvent({
            variables: {
                userId: user.id,
                eventId: calendarEvent.id
            }
        }).then(refetchParticipants);
    }, [addUserToCalendarEvent, refetchParticipants]);

    const [removeUserFromCalendarEvent] = useRemoveUserFromCalendarEventMutation();

    const [deleteCalendarEvent] = useDeleteCalendarEventMutation();

    const onUserRemoved = useCallback((user: AppUser) => {
        removeUserFromCalendarEvent({
            variables: {
                userId: user.id,
                eventId: calendarEvent.id
            }
        }).then(refetchParticipants);
    }, [calendarEvent, refetchParticipants]);


    const onUsersManageSubmit = useCallback((users: User[]) => {
        users.forEach(onUserAdd);
        refetchParticipants();
    }, [refetchParticipants]);

    const join = useCallback(() => {
        user && onUserAdd(user);
    }, [user]);

    const leave = useCallback(() => {
        user && onUserRemoved(user);
    }, [user]);

    const processDelete = useCallback(() => {
        deleteCalendarEvent({
            variables: {
                id: calendarEvent.id
            }
        }).then(() => {
            router.push("/events");
        });
    }, []);

    return (
        <>
            <EventDetailHeader calendarEvent={calendarEvent} isJoined={isJoined} canEdit={canEdit} join={join} leave={leave} processDelete={processDelete} />
            <EventDetailInfo calendarEvent={calendarEvent} channel={channel} />
            <EventDetailParticipants calendarEvent={calendarEvent} canEdit={canEdit} participants={participants} saveUsers={onUsersManageSubmit} removeUser={onUserRemoved} />
        </>
    );
};

EventDetail.displayName = "EventDetailComponent";
export default EventDetail;