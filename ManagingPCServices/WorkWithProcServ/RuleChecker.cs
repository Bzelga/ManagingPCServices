using AngouriMath.Extensions;
using ManagingPCServices.DBWorker;
using ManagingPCServices.Hubs;
using ManagingPCServices.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace ManagingPCServices
{
    public class RuleChecker
    {
        private readonly List<Rule> _rules;
        private readonly List<Parameter> _parameters;
        private readonly Timer timer;
        private readonly IHubContext<ServiceHub, IServiceHub> _hub;

        public RuleChecker(IHubContext<ServiceHub, IServiceHub> hub)
        {
            using (TestDBContext tdbc = new TestDBContext())
            {
                _rules = tdbc.Rules.Include(a => a.ActionNavigation).ToList();
                _parameters = tdbc.Parameters
                    .Include(t => t.TypeParameterNavigation)
                    .Include(t => t.SourceNavigation)
                    .Include(t => t.TypeValueNavigation)
                    .ToList();
            }

            _hub = hub;

            timer = new Timer(new TimeSpan(0, 0, 30).TotalMilliseconds);
            timer.Elapsed += GetParam;
            timer.Start();
        }

        private async void GetParam(object source, ElapsedEventArgs args)
        {
            await _hub.Clients.All.Do(new SendCommandPackage
            {
                TypeCommand = 5,
                ArgsForAction = "4"
            });
        }

        public ReceiveCommandPackage CheckRule(string[] parameters)
        {
            var convertedParameter = concertStringParametersToClass(parameters);

            bool resultPredic = false;

            for (int i = 0; i < _rules.Count; i++)
            {
                string[] elemPredicate = _rules[i].Predicate.Split(' ');

                for (int j = 0; j < elemPredicate.Length; j++)
                {
                    var elem = _parameters.Where(e => e.Designation == elemPredicate[j]).FirstOrDefault();

                    if (elem != null)
                    {
                        var foundParam = convertedParameter.Where(n => n.Designation == elemPredicate[j]).Select(v => v.Value).FirstOrDefault();
                        elemPredicate[j] = (elem.MinValue > foundParam || elem.MaxValue < foundParam).ToString();
                    }
                }

                string output = String.Join(" ", elemPredicate);
                resultPredic = output.EvalBoolean();

                if (resultPredic)
                    return new ReceiveCommandPackage
                    {
                        TypeCommand = 4,
                        ReturnAction = new ActionModel
                        {
                            TextAction = $"{_rules[i].ActionNavigation.TextAction} {parameters[i].Split(',')[1]}",
                            TypeAction = (int)_rules[i].ActionNavigation.NumberAction,
                            TypeCommand = (int)_rules[i].ActionNavigation.TypeCommand,
                            Args = parameters[i].Split(',')[1]

                        }
                    };
            }

            return new ReceiveCommandPackage ();
        }

        private List<DesignationParamAndValue> concertStringParametersToClass(string[] parameters)
        {
            List<DesignationParamAndValue> DPV = new();

            foreach (var param in parameters)
            {
                string[] elemArg = param.Split(',');
                string designation = _parameters.Where(a => a.TypeParameterNavigation.Name == elemArg[0]
                    && a.SourceNavigation.Name == elemArg[1]
                    && a.TypeValueNavigation.Name == elemArg[2]).Select(d => d.Designation).FirstOrDefault();

                if (designation == null && elemArg[0] != "sensors")
                {
                    designation = _parameters.Where(a => a.TypeParameterNavigation.Name == elemArg[0]
                    && a.SourceNavigation.Name == "default"
                    && a.TypeValueNavigation.Name == elemArg[2]).Select(d => d.Designation).FirstOrDefault();
                }

                DPV.Add(new DesignationParamAndValue
                {
                    Designation = designation,
                    Value = Convert.ToDouble(elemArg[3])
                });
            }

            return DPV;
        }
    }
}
