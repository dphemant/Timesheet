﻿// Copyright(c) Microsoft Corporation. 
// All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the solution root folder for full license information.

using GoLocal.TimeWise.AzureFunctions.Models;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoLocal.TimeWise.AzureFunctions.Helpers
{

    public static class HoursComputeHelper
    {

        // This method takes a Resport Hours object as input and returns the final hours(Adjusted/computed) along with the total
        public static ConcurrentDictionary<string, short> GetFinalHrsMins(ReportHours wh)
        {
            ConcurrentDictionary<string, short> FinalDictObj = new ConcurrentDictionary<string, short>();

            //For Email Hours
            if (wh.Fields.EmailAdjustedHours > 0 || wh.Fields.EmailAdjustedMinutes > 0)
            {

                FinalDictObj.TryAdd("FinalEmailHrs", wh.Fields.EmailAdjustedHours);
                FinalDictObj.TryAdd("FinalEmailMins", wh.Fields.EmailAdjustedMinutes);

            }
            else
            {
                FinalDictObj.TryAdd("FinalEmailHrs", wh.Fields.EmailHours);
                FinalDictObj.TryAdd("FinalEmailMins", wh.Fields.EmailMinutes);

            }
            //For Meeting Hours
            if (wh.Fields.MeetingAdjustedHours > 0 || wh.Fields.MeetingAdjustedMinutes > 0)
            {

                FinalDictObj.TryAdd("FinalMeetingHrs", wh.Fields.MeetingAdjustedHours);
                FinalDictObj.TryAdd("FinalMeetingMins", wh.Fields.MeetingAdjustedMinutes);

            }
            else
            {
                FinalDictObj.TryAdd("FinalMeetingHrs", wh.Fields.MeetingHours);
                FinalDictObj.TryAdd("FinalMeetingMins", wh.Fields.MeetingMinutes);

            }
            //For Other Hours
            if (wh.Fields.OtherAdjustedHours > 0 || wh.Fields.OtherAdjustedMinutes > 0)
            {

                FinalDictObj.TryAdd("FinalOtherHrs", wh.Fields.OtherAdjustedHours);
                FinalDictObj.TryAdd("FinalOtherMins", wh.Fields.OtherAdjustedMinutes);

            }
            else
            {
                FinalDictObj.TryAdd("FinalOtherHrs", wh.Fields.OtherHours);
                FinalDictObj.TryAdd("FinalOtherMins", wh.Fields.OtherMinutes);

            }

            // Total Calculation
            var Mins = FinalDictObj["FinalEmailMins"] + FinalDictObj["FinalMeetingMins"] + FinalDictObj["FinalOtherMins"];
            var totalMins = Mins % 60;
            var totalHrs = FinalDictObj["FinalEmailHrs"] + FinalDictObj["FinalMeetingHrs"] + FinalDictObj["FinalOtherHrs"] + Mins / 60;

            FinalDictObj.TryAdd("FinalTotalHrs", (short)totalHrs);
            FinalDictObj.TryAdd("FinalTotalMins", (short)totalMins);

            return FinalDictObj;
        }

        public static ConcurrentDictionary<string, short> GetFinalTeamHrsMins(TeamHours th)
        {
            ConcurrentDictionary<string, short> FinalDictObj = new ConcurrentDictionary<string, short>();

            //For Email Hours
            if (th.Fields.EmailAdjustedHours > 0 || th.Fields.EmailAdjustedMinutes > 0)
            {

                FinalDictObj.TryAdd("FinalEmailHrs", th.Fields.EmailAdjustedHours);
                FinalDictObj.TryAdd("FinalEmailMins", th.Fields.EmailAdjustedMinutes);

            }
            else
            {
                FinalDictObj.TryAdd("FinalEmailHrs", th.Fields.EmailHours);
                FinalDictObj.TryAdd("FinalEmailMins", th.Fields.EmailMinutes);

            }
            //For Meeting Hours
            if (th.Fields.MeetingAdjustedHours > 0 || th.Fields.MeetingAdjustedMinutes > 0)
            {

                FinalDictObj.TryAdd("FinalMeetingHrs", th.Fields.MeetingAdjustedHours);
                FinalDictObj.TryAdd("FinalMeetingMins", th.Fields.MeetingAdjustedMinutes);

            }
            else
            {
                FinalDictObj.TryAdd("FinalMeetingHrs", th.Fields.MeetingHours);
                FinalDictObj.TryAdd("FinalMeetingMins", th.Fields.MeetingMinutes);

            }
            //For Other Hours
            if (th.Fields.OtherAdjustedHours > 0 || th.Fields.OtherAdjustedMinutes > 0)
            {

                FinalDictObj.TryAdd("FinalOtherHrs", th.Fields.OtherAdjustedHours);
                FinalDictObj.TryAdd("FinalOtherMins", th.Fields.OtherAdjustedMinutes);

            }
            else
            {
                FinalDictObj.TryAdd("FinalOtherHrs", th.Fields.OtherHours);
                FinalDictObj.TryAdd("FinalOtherMins", th.Fields.OtherMinutes);

            }

            // Total Calculation
            var Mins = FinalDictObj["FinalEmailMins"] + FinalDictObj["FinalMeetingMins"] + FinalDictObj["FinalOtherMins"];
            var totalMins = Mins % 60;
            var totalHrs = FinalDictObj["FinalEmailHrs"] + FinalDictObj["FinalMeetingHrs"] + FinalDictObj["FinalOtherHrs"] + Mins / 60;

            FinalDictObj.TryAdd("FinalTotalHrs", (short)totalHrs);
            FinalDictObj.TryAdd("FinalTotalMins", (short)totalMins);

            return FinalDictObj;
        }

        public static ConcurrentDictionary<string, string> ComputeHoursAndMins(IEnumerable<WorkHours> WrkHoursList)
        {

            ConcurrentDictionary<string, int> FinalDictObj = new ConcurrentDictionary<string, int>();
            if (WrkHoursList.Count() == 0)
            {
                FinalDictObj.TryAdd("EmailMins", 0);
                FinalDictObj.TryAdd("EmailHrs", 0);
                FinalDictObj.TryAdd("EmailAdjustedMins", 0);
                FinalDictObj.TryAdd("EmailAdjustedHrs", 0);
                FinalDictObj.TryAdd("MeetingMins", 0);
                FinalDictObj.TryAdd("MeetingHrs", 0);
                FinalDictObj.TryAdd("MeetingAdjustedMins", 0);
                FinalDictObj.TryAdd("MeetingAdjustedHrs", 0);
                FinalDictObj.TryAdd("OtherMins", 0);
                FinalDictObj.TryAdd("OtherHrs", 0);
                FinalDictObj.TryAdd("OtherAdjustedMins", 0);
                FinalDictObj.TryAdd("OtherAdjustedHrs", 0);
                FinalDictObj.TryAdd("TotHrs", 0);
                FinalDictObj.TryAdd("TotMins", 0);


                return ConvertToStringOfStrings(FinalDictObj);
            }

            int Mins = 0;
            int TotalMins = 0;
            int TotalHrs = 0;

            //calculate total hrs and mins of this week

            //Int16 fieldValue = 0;
            int dictValue = 0;

            //Email Hours and Mins Calc
            var totalEmailMinsForWeek = WrkHoursList.Sum(item => item.Fields.GetFinalEmailMinutes());

            FinalDictObj.GetOrAdd("EmailMins", totalEmailMinsForWeek % 60);

            if (FinalDictObj.TryGetValue("EmailMins", out dictValue))
                Mins += dictValue;

            var totalEmailHrsforWeek = WrkHoursList.Sum(item => item.Fields.GetFinalEmailHours());

            FinalDictObj.GetOrAdd("EmailHrs", totalEmailHrsforWeek + (totalEmailMinsForWeek / 60));

            if (FinalDictObj.TryGetValue("EmailHrs", out dictValue))
                TotalHrs += dictValue;

            //Meeting Hours and Mins Calc

            var totalMeetingMinsforWeek = WrkHoursList.Sum(item => item.Fields.GetFinalMeetingMinutes());

            FinalDictObj.GetOrAdd("MeetingMins", totalMeetingMinsforWeek % 60);

            if (FinalDictObj.TryGetValue("MeetingMins", out dictValue))
                Mins += dictValue;

            var totalMeetingHrsforWeek = WrkHoursList.Sum(item => item.Fields.GetFinalMeetingHours());

            FinalDictObj.GetOrAdd("MeetingHrs", totalMeetingHrsforWeek + (totalMeetingMinsforWeek / 60));

            if (FinalDictObj.TryGetValue("MeetingHrs", out dictValue))
                TotalHrs += dictValue;

            //Other Hours and Mins Calc
            var totalOtherMinsforWeek = WrkHoursList.Sum(item => item.Fields.GetFinalOtherMinutes());

            FinalDictObj.GetOrAdd("OtherMins", (totalOtherMinsforWeek) % 60);

            if (FinalDictObj.TryGetValue("OtherMins", out dictValue))
                Mins += dictValue;

            var totalOtherHrsforWeek = WrkHoursList.Sum(item => item.Fields.GetFinalOtherHours());


            FinalDictObj.GetOrAdd("OtherHrs", totalOtherHrsforWeek + (totalOtherMinsforWeek / 60));

            if (FinalDictObj.TryGetValue("OtherHrs", out dictValue))
                TotalHrs += dictValue;

            TotalHrs += Mins / 60;
            TotalMins = Mins % 60;

            FinalDictObj.TryAdd("TotHrs", TotalHrs);
            FinalDictObj.TryAdd("TotMins", TotalMins);

            return ConvertToStringOfStrings(FinalDictObj);
        }

        public static Dictionary<string, short> ComputeDailyTotals(WorkHoursFields whf, TimeTrackerOptions timeTrackerOptions)
        {
            Dictionary<string, short> FinalDictObj = new Dictionary<string, short>();

            //For Email Hours
            if (whf.EmailAdjustedHours > 0 || whf.EmailAdjustedMinutes > 0)
            {

                FinalDictObj.Add("FinalEmailHrs", whf.EmailAdjustedHours);
                FinalDictObj.Add("FinalEmailMins", whf.EmailAdjustedMinutes);

            }
            else
            {
                if (timeTrackerOptions.EnableTimer == true)
                {
                    FinalDictObj.Add("FinalEmailHrs", whf.EmailTimerHours);
                    FinalDictObj.Add("FinalEmailMins", whf.EmailTimerMinutes);
                }
                else
                {
                    FinalDictObj.Add("FinalEmailHrs", whf.EmailHours);
                    FinalDictObj.Add("FinalEmailMins", whf.EmailMinutes);
                }

            }
            //For Meeting Hours
            if (whf.MeetingAdjustedHours > 0 || whf.MeetingAdjustedMinutes > 0)
            {
                FinalDictObj.Add("FinalMeetingHrs", whf.MeetingAdjustedHours);
                FinalDictObj.Add("FinalMeetingMins", whf.MeetingAdjustedMinutes);

            }
            else
            {
                if (timeTrackerOptions.EnableTimer == true)
                {
                    FinalDictObj.Add("FinalMeetingHrs", whf.MeetingTimerHours);
                    FinalDictObj.Add("FinalMeetingMins", whf.MeetingTimerMinutes);
                }
                else
                {
                    FinalDictObj.Add("FinalMeetingHrs", whf.MeetingHours);
                    FinalDictObj.Add("FinalMeetingMins", whf.MeetingMinutes);
                }

            }
            //For Other Hours
            if (whf.OtherAdjustedHours > 0 || whf.OtherAdjustedMinutes > 0)
            {

                FinalDictObj.Add("FinalOtherHrs", whf.OtherAdjustedHours);
                FinalDictObj.Add("FinalOtherMins", whf.OtherAdjustedMinutes);

            }
            else
            {
                if (timeTrackerOptions.EnableTimer == true)
                {
                    FinalDictObj.Add("FinalOtherHrs", whf.OtherTimerHours);
                    FinalDictObj.Add("FinalOtherMins", whf.OtherTimerMinutes);
                }
                else
                {
                    FinalDictObj.Add("FinalOtherHrs", whf.OtherHours);
                    FinalDictObj.Add("FinalOtherMins", whf.OtherMinutes);
                }

            }

            // Total Calculation
            var Mins = FinalDictObj["FinalEmailMins"] + FinalDictObj["FinalMeetingMins"] + FinalDictObj["FinalOtherMins"];
            var totalMins = Mins % 60;
            var totalHrs = FinalDictObj["FinalEmailHrs"] + FinalDictObj["FinalMeetingHrs"] + FinalDictObj["FinalOtherHrs"] + Mins / 60;

            FinalDictObj.Add("FinalTotalHrs", (short)totalHrs);
            FinalDictObj.Add("FinalTotalMins", (short)totalMins);

            return FinalDictObj;

        }

        private static ConcurrentDictionary<string, string> ConvertToStringOfStrings(ConcurrentDictionary<string, int> input)
        {
            var retVal = new ConcurrentDictionary<string, string>();
            foreach (var key in input.Keys)
            {
                retVal.GetOrAdd(key, input[key].ToString());
            }

            return retVal;
        }
    }
}
